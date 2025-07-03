using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IKafkaProducerApi : IDisposable
{
    ValueTask<KafkaProduceOut> ProduceAsync<T>(
        KafkaProduceIn<T> input, CancellationToken cancellationToken);

    ValueTask<Result<KafkaProduceOut, Failure<KafkaProduceFailureCode>>> ProduceOrFailureAsync<T>(
        KafkaProduceIn<T> input, CancellationToken cancellationToken);
}