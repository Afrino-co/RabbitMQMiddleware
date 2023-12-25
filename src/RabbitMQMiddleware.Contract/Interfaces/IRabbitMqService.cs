using RabbitMQ.Client;

namespace RabbitMQMiddleware.Contract.Interfaces;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}
