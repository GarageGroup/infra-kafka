namespace GarageGroup.Infra;

public sealed record class KafkaProduceOut
{
    public required string Topic { get; init; }

    public required int Partition { get; init; }

    public required long Offset { get; init; }
}