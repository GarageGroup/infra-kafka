using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class KafkaConsumerDependency
{
    public static Dependency<IKafkaConsumerHandler> UseKafkaConsumerHandler<T>(
        this Dependency<IHandler<T, Unit>> dependency,
        Func<IServiceProvider, KafkaConsumerOption> optionResolver,
        Func<IServiceProvider, ILoggerFactory>? loggerFactoryResolver = null)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        ArgumentNullException.ThrowIfNull(optionResolver);

        return dependency.Map<IKafkaConsumerHandler>(ResolveHandler);

        KafkaConsumerHandler<T> ResolveHandler(IServiceProvider serviceProvider, IHandler<T, Unit> innerHandler)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(innerHandler);

            var loggerFactory = loggerFactoryResolver?.Invoke(serviceProvider) ?? serviceProvider.GetService<ILoggerFactory>();

            return new(
                innerHandler: innerHandler,
                logger: loggerFactory?.CreateLogger<KafkaConsumerHandler<T>>(),
                option: optionResolver.Invoke(serviceProvider));
        }
    }

    public static Dependency<IKafkaConsumerHandler> UseKafkaConsumerHandler<T>(
        this Dependency<IHandler<T, Unit>> dependency, string kafkaConsumeSection)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IKafkaConsumerHandler>(ResolveHandler);

        KafkaConsumerHandler<T> ResolveHandler(IServiceProvider serviceProvider, IHandler<T, Unit> innerHandler)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(innerHandler);

            var section = serviceProvider.GetRequiredService<IConfiguration>().GetRequiredSection(kafkaConsumeSection.OrEmpty());

            return new(
                innerHandler: innerHandler,
                logger: serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<KafkaConsumerHandler<T>>(),
                option: section.GetKafkaConsumerOption());
        }
    }

    private static KafkaConsumerOption GetKafkaConsumerOption(this IConfigurationSection section)
    {
        return new()
        {
            BootstrapServers = GetOrThrow("BootstrapServers"),
            GroupId = GetOrThrow("GroupId"),
            Topic = GetOrThrow("Topic"),
            MaxRetry = int.TryParse(section["MaxRetry"], out var maxRetry) ? maxRetry : null,
            RetryDelay = TimeSpan.TryParse(section["RetryDelay"], out var retryDelay) ? retryDelay : null
        };

        string GetOrThrow(string key)
        {
            var value = section[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Key '{section.Key}:{key}' value must be specified.");
            }

            return value;
        }
    }
}