namespace GarageGroup.Infra;

public sealed record class KafkaProducerOption
{
    public required string BootstrapServers { get; init; }
}