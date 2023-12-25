# RabbitMQMiddleware
RabbitMQ middleware for .net suitable for clean architecture

# Install the package
Find the package through NuGet Package Manager or install it with following command.

Interface
```
dotnet add package RabbitMQMiddleware.Contract
```

Implementation
```
dotnet add package RabbitMQMiddleware.Implementation
```
note: in clean architecture add "Interface" library to "Application" layer/project and add "Implementation" library to Infrastructure layer/project

# Register Services
Add the following in Program.cs
```
builder.Services.AddRabbitMQMiddleware(builder.Configuration);
```

# Add RabbitMq Configuration
Add the following in appsettings.json
```
  "RabbitMqConfiguration": {
    "HostName": "192.168.1.1",
    "VirtualHost": "/",
    "Port": "5672",
    "Username": "username",
    "Password": "password"
  }
```
# How To Use
Inject IProducerService
```
  private readonly IProducerService<string> _producerService;
  public SetDelayedCommandHandler(IProducerService<string> producerService)
  {
    _producerService = producerService;
  }
  public void SendMessage(string message)
  {
    _producerService.Send(message: message, exchangeName: "Exchange Name", exchangeType: "direct", queueName: "Queue Name", routingKey: "Routing Key");
  }
```



