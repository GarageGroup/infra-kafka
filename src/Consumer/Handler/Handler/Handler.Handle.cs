using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Infra;

partial class KafkaConsumerHandler<T>
{
    public async ValueTask<Result<Unit, Failure<HandlerFailureCode>>> HandleAsync(
        Unit input, CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe(topic);

        logger?.LogInformation("Start Kafka consuming...");
        var retryTracker = new ConcurrentDictionary<TopicPartitionOffset, int>();

        try
        {
            while (true)
            {
                var message = consumer.Consume(cancellationToken);

                var result = await DeserializeOrFailure(message.Message.Value).ForwardValueAsync(InnerHandleAsync).ConfigureAwait(false);
                _ = result.Fold(InnerOnSuccess, InnerOnFailure);

                Unit InnerOnSuccess(Unit @out)
                {
                    consumer.Commit(message);

                    _ = retryTracker.TryRemove(message.TopicPartitionOffset, out _);
                    return @out;
                }

                Unit InnerOnFailure(Failure<HandlerFailureCode> failure)
                {
                    var key = message.Message.Key;
                    logger?.LogError(failure.SourceException, "Kafka message {key} consumer error: '{message}'.", key, failure.FailureMessage);

                    if (failure.FailureCode is HandlerFailureCode.Persistent)
                    {
                        return OffsetFailureMessage(message);
                    }

                    var retryCount = retryTracker.AddOrUpdate(message.TopicPartitionOffset, 1, InnerIncrement);
                    if (retryCount >= maxRetry)
                    {
                        logger?.LogInformation("Max retries reached for message {key}. Skipping message.", key);
                        return OffsetFailureMessage(message);
                    }

                    logger?.LogInformation("Pausing for message {key}. Skipping message for retry {count}/{max}.", key, retryCount, maxRetry);

                    TopicPartition[] partitions = [message.TopicPartition];
                    consumer.Pause(partitions);

                    _ = InnerDelayAsync(partitions).ConfigureAwait(false);
                    return default;
                }

                Unit OffsetFailureMessage(ConsumeResult<string, string> message)
                {
                    retryTracker.TryRemove(message.TopicPartitionOffset, out _);
                    consumer.Seek(new(message.TopicPartition, message.Offset + 1));

                    return default;
                }

                async Task InnerDelayAsync(TopicPartition[] partitions)
                {
                    consumer.Seek(message.TopicPartitionOffset);
                    await Task.Delay(retryDelay, cancellationToken).ConfigureAwait(false);
                    consumer.Resume(partitions);
                }
            }
        }
        catch (OperationCanceledException ex)
        {
            logger?.LogWarning(ex, "Shutting down...");
        }

        return Result.Success<Unit>(default);

        async ValueTask<Result<Unit, Failure<HandlerFailureCode>>> InnerHandleAsync(T? input)
        {
            try
            {
                return await innerHandler.HandleAsync(input, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.ToFailure(HandlerFailureCode.Transient, "An unexpected exception was thrown when trying to consume Kafka message.");
            }
        }

        static int InnerIncrement(TopicPartitionOffset _, int current)
            =>
            current + 1;
    }

    private static Result<T?, Failure<HandlerFailureCode>> DeserializeOrFailure(string? source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(source, SerializerOptions);
        }
        catch (Exception ex)
        {
            return ex.ToFailure(HandlerFailureCode.Persistent, $"Failed to deserialize Kafka message of type {typeof(T).FullName}.");
        }
    }
}
