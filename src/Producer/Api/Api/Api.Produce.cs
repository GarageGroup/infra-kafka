using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class KafkaProducerApi
{
    public async ValueTask<KafkaProduceOut> ProduceAsync<T>(KafkaProduceIn<T> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        var message = SerializeMessage(input);
        var result = await producer.Value.ProduceAsync(input.Topic, message, cancellationToken).ConfigureAwait(false);

        return MapDeliveryResult(result);
    }
}