namespace RabbitMQMiddleware.Implementation.Common;

public class RabbitMqConfiguration
{
    public string HostName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public int Port { get; set; }
}
