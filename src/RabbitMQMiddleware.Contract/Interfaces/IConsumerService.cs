using RabbitMQ.Client;

namespace RabbitMQMiddleware.Contract.Interfaces;

public interface IConsumerService
{
    IModel GetRabbitModel(string exchangeName, string exchangeType, string queueName,
        string routingKey);
}
