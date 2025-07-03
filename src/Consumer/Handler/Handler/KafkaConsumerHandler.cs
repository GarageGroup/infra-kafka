using System;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Infra;

internal sealed partial class KafkaConsumerHandler<T> : IKafkaConsumerHandler
{
    private static readonly JsonSerializerOptions SerializerOptions
        =
        new()
        {
            PropertyNameCaseInsensitive = true
        };

    private const int DefaultMaxRetry = 5;

    private static readonly TimeSpan DefaultRetryDelay
        =
        TimeSpan.FromSeconds(5);

    private readonly IHandler<T, Unit> innerHandler;

    private readonly ConsumerConfig consumerConfig;

    private readonly string topic;

    private readonly int maxRetry;

    private readonly TimeSpan retryDelay;

    private readonly ILogger<KafkaConsumerHandler<T>>? logger;

    internal KafkaConsumerHandler(IHandler<T, Unit> innerHandler, ILogger<KafkaConsumerHandler<T>>? logger, KafkaConsumerOption option)
    {
        this.innerHandler = innerHandler;
        this.logger = logger;

        consumerConfig = new()
        {
            BootstrapServers = option.BootstrapServers,
            GroupId = option.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        topic = option.Topic ?? string.Empty;
        maxRetry = option.MaxRetry > 0 ? option.MaxRetry.Value : DefaultMaxRetry;
        retryDelay = option.RetryDelay ?? DefaultRetryDelay;
    }
}