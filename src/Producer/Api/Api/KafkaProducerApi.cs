using System;
using System.Text.Json;
using System.Threading;
using Confluent.Kafka;

namespace GarageGroup.Infra;

internal sealed partial class KafkaProducerApi : IKafkaProducerApi
{
    private static readonly JsonSerializerOptions SerializerOptions
        =
        new(JsonSerializerDefaults.Web);

    private readonly Lazy<IProducer<string, string>> producer;

    private bool isDisposed;

    internal KafkaProducerApi(KafkaProducerOption option)
    {
        producer = new(BuildProducer, LazyThreadSafetyMode.ExecutionAndPublication);

        IProducer<string, string> BuildProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = option.BootstrapServers.OrEmpty()
            };

            return new ProducerBuilder<string, string>(config).Build();
        }
    }

    private static Message<string, string> SerializeMessage<T>(KafkaProduceIn<T> input)
        =>
        new()
        {
            Key = input.Key.OrEmpty(),
            Value = JsonSerializer.Serialize(input.Value, SerializerOptions)
        };

    private static KafkaProduceOut MapDeliveryResult(DeliveryResult<string, string> result)
        =>
        new()
        {
            Topic = result.Topic.OrEmpty(),
            Partition = result.Partition.Value,
            Offset = result.Offset.Value
        };
}