using Microsoft.Extensions.Configuration;
using RabbitMQMiddleware.Contract.Interfaces;
using RabbitMQMiddleware.Implementation.Common;
using RabbitMQMiddleware.Implementation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddRabbitMQMiddleware(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfiguration>(a => configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.AddSingleton(typeof(IProducerService<>), typeof(ProducerService<>));
        return services;
    }
}
