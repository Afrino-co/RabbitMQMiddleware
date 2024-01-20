using RabbitMQ.Client;
using RabbitMQMiddleware.Contract.Interfaces;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace RabbitMQMiddleware.Implementation.Services;

public class ProducerService<T> : IProducerService<T>, IDisposable
{
    private readonly IModel _model;
    private readonly IConnection _connection;
    public ProducerService(IRabbitMqService rabbitMqService)
    {
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();

    }

    public async Task SendAsync(T message, string exchangeName, string exchangeType, string queueName, string routingKey)
    {        
        await Task.Delay(1);
        _model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
        _model.QueueBind(queueName, exchangeName, routingKey);

        var messageJson = JsonSerializer.Serialize(message);
        var props = _model.CreateBasicProperties();
        props.Persistent = true; // or props.DeliveryMode = 2;
        _model.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: props,
            body: Encoding.UTF8.GetBytes(messageJson),
            mandatory: true);
    }


    public async Task SendSerializedMessageAsync(string message, string exchangeName, string exchangeType, string queueName, string routingKey)
    {
        await Task.Delay(1);
        _model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
        _model.QueueBind(queueName, exchangeName, routingKey);
        var props = _model.CreateBasicProperties();
        props.Persistent = true; // or props.DeliveryMode = 2;
        _model.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: props,
            body: Encoding.UTF8.GetBytes(message),
            mandatory: true);
    }
    public void Send(T message, string exchangeName, string exchangeType, string queueName, string routingKey)
    {
        _model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
        _model.QueueBind(queueName, exchangeName, routingKey);

        var messageJson = JsonSerializer.Serialize(message);
        var props = _model.CreateBasicProperties();
        props.Persistent = true; // or props.DeliveryMode = 2;
        _model.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: props,
            body: Encoding.UTF8.GetBytes(messageJson),
            mandatory: true);
    }

    public void SendSerializedMessage(string message, string exchangeName, string exchangeType, string queueName, string routingKey)
    {
        _model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
        _model.QueueBind(queueName, exchangeName, routingKey);
        var props = _model.CreateBasicProperties();
        props.Persistent = true; // or props.DeliveryMode = 2;
        _model.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: props,
            body: Encoding.UTF8.GetBytes(message),
            mandatory: true);
    }
    public void SendSerializedMessage(string? hostName, string? virtualHost, int? port, string? username, string? password, string message, string exchangeName, string exchangeType, string queueName, string routingKey)
    {
        if (string.IsNullOrWhiteSpace(hostName) || string.IsNullOrWhiteSpace(virtualHost) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || port == null || port <= 0)
        {
            SendSerializedMessage(message, exchangeName, exchangeType, queueName, routingKey);            
        }
        else
        {
            ConnectionFactory connection = new()
            {
                UserName = username,
                Password = password,
                HostName = hostName,
                VirtualHost = virtualHost,
                Port = port.Value,
                DispatchConsumersAsync = true
            };
            var channel = connection.CreateConnection();
            var model = channel.CreateModel();

            model.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
            model.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false);
            model.QueueBind(queueName, exchangeName, routingKey);
            var props = _model.CreateBasicProperties();
            props.Persistent = true; // or props.DeliveryMode = 2;
            model.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: props,
                body: Encoding.UTF8.GetBytes(message),
                mandatory: true);
        }
        
    }
    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }


}
