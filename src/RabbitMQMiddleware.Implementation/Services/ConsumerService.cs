using RabbitMQ.Client;
using RabbitMQMiddleware.Contract.Interfaces;

namespace RabbitMQMiddleware.Implementation.Services;

public class ConsumerService : IConsumerService
{
    private readonly IModel _model;
    private readonly IConnection _connection;
    public ConsumerService(IRabbitMqService rabbitMqService)
    {
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();

    }
    public IModel GetRabbitModel(string exchangeName, string exchangeType, string queueName, string routingKey)
    {
        _model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
        _model.QueueBind(queueName, exchangeName, routingKey);

        return _model;
    }
}
