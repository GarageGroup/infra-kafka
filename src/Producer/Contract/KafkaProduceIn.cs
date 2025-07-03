namespace GarageGroup.Infra;

public sealed record class KafkaProduceIn<T>
{
    public required string Topic { get; init; }

    public required string Key { get; init; }

    public required T Value { get; init; }
}