using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace GarageGroup.Infra;

partial class KafkaProducerApi
{
    public async ValueTask<Result<KafkaProduceOut, Failure<KafkaProduceFailureCode>>> ProduceOrFailureAsync<T>(
        KafkaProduceIn<T> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        var result = await SerializeMessageOrFailure(input).ForwardValueAsync(InnerProduceAsync).ConfigureAwait(false);
        return result.MapSuccess(MapDeliveryResult);

        ValueTask<Result<DeliveryResult<string, string>, Failure<KafkaProduceFailureCode>>> InnerProduceAsync(
            Message<string, string> message)
            =>
            InnerProduceOrFailureAsync(input.Topic, message, cancellationToken);
    }

    private static Result<Message<string, string>, Failure<KafkaProduceFailureCode>> SerializeMessageOrFailure<T>(
        KafkaProduceIn<T> input)
    {
        try
        {
            return SerializeMessage(input);
        }
        catch (Exception ex)
        {
            return ex.ToFailure(
                failureCode: KafkaProduceFailureCode.InvalidMsg,
                failureMessage: $"Failed to deserialize message of type {typeof(T).FullName}.");
        }
    }

    private async ValueTask<Result<DeliveryResult<string, string>, Failure<KafkaProduceFailureCode>>> InnerProduceOrFailureAsync(
        string topic, Message<string, string> message, CancellationToken cancellationToken)
    {
        try
        {
            return await producer.Value.ProduceAsync(topic, message, cancellationToken).ConfigureAwait(false);
        }
        catch (KafkaException ex)
        {
            return ex.ToFailure(
                failureCode: (KafkaProduceFailureCode)ex.Error.Code,
                failureMessage: $"Failed to produce message with key '{message.Key}'.");
        }
    }
}