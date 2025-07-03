using System;
using Microsoft.Extensions.Configuration;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class KafkaProducer
{
    public static Dependency<IKafkaProducerApi> UseApi(Func<IServiceProvider, KafkaProducerOption> optionResolver)
    {
        ArgumentNullException.ThrowIfNull(optionResolver);
        return Dependency.From<IKafkaProducerApi>(ResolveApi);

        KafkaProducerApi ResolveApi(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            return new(
                option: optionResolver.Invoke(serviceProvider));
        }
    }

    public static Dependency<IKafkaProducerApi> UseApi(string sectionName)
    {
        return Dependency.From<IKafkaProducerApi>(ResolveApi);

        KafkaProducerApi ResolveApi(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            return new(
                option: serviceProvider.ResolveKafkaProducerOption(sectionName.OrEmpty()));
        }
    }

    private static KafkaProducerOption ResolveKafkaProducerOption(this IServiceProvider serviceProvider, string sectionName)
    {
        if (serviceProvider.GetService(typeof(IConfiguration)) is not IConfiguration configuration)
        {
            throw new InvalidOperationException($"Failed to resolve {typeof(IConfiguration).FullName} type.");
        }

        var bootstrapServers = configuration[$"{sectionName}:BootstrapServers"];
        if (string.IsNullOrWhiteSpace(bootstrapServers))
        {
            throw new InvalidOperationException("BootstrapServers must be specified in the configuration.");
        }

        return new()
        {
            BootstrapServers = bootstrapServers
        };
    }
}