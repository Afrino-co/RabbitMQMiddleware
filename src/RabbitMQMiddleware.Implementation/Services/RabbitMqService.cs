using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQMiddleware.Contract.Interfaces;
using RabbitMQMiddleware.Implementation.Common;

namespace RabbitMQMiddleware.Implementation.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfiguration _configuration;
    public RabbitMqService(IOptions<RabbitMqConfiguration> options)
    {
        _configuration = options.Value;
    }
    public IConnection CreateChannel()
    {
        ConnectionFactory connection = new()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
            HostName = _configuration.HostName,
            VirtualHost = _configuration.VirtualHost,
            Port = _configuration.Port,
            DispatchConsumersAsync = true,
            ContinuationTimeout = TimeSpan.FromHours(2)
        };
        var channel = connection.CreateConnection();
        return channel;
    }
}