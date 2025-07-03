using System;

namespace GarageGroup.Infra;

public sealed record class KafkaConsumerOption
{
    public required string BootstrapServers { get; init; }

    public required string GroupId { get; init; }

    public required string Topic { get; init; }

    public int? MaxRetry { get; init; }

    public TimeSpan? RetryDelay { get; init; }
}