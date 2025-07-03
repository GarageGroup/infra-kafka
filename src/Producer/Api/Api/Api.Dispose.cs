namespace GarageGroup.Infra;

partial class KafkaProducerApi
{
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        if (producer.IsValueCreated)
        {
            producer.Value.Dispose();
        }

        isDisposed = true;
    }
}