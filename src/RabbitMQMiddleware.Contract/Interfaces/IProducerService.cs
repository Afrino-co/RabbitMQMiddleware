namespace RabbitMQMiddleware.Contract.Interfaces;

public interface IProducerService<T>
{
    Task SendAsync(T message, string exchangeName, string exchangeType, string queueName,
        string routingKey);

    Task SendSerializedMessageAsync(string message, string exchangeName, string exchangeType, string queueName,
        string routingKey);

    void Send(T message, string exchangeName, string exchangeType, string queueName,
        string routingKey);

    void SendSerializedMessage(string message, string exchangeName, string exchangeType, string queueName,
        string routingKey);
    void SendSerializedMessage(string? hostName, string? virtualHost, int? port, string? username,
        string? password, string message, string exchangeName, string exchangeType, string queueName, string routingKey);
}

