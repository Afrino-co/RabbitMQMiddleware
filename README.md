# RabbitMQMiddleware
RabbitMQ middleware for .net, suitable for clean architecture

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
note: in clean architecture add "Interface" library to "Application" layer/project and add "Implementation" library to "Infrastructure" layer/project

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
For send message inject IProducerService
```
  private readonly IProducerService<string> _producerService;
  public ClassName(IProducerService<string> producerService)
  {
    _producerService = producerService;
  }
  public void SendMessage(string message)
  {
    _producerService.Send(message: message, exchangeName: "Exchange Name",
    exchangeType: "direct", queueName: "Queue Name", routingKey: "Routing Key");
  }
```

For receive message inject IConsumerService
```
  private readonly IConsumerService _consumerService;
  public ClassName(IConsumerService consumerService)
  {
    _consumerService = consumerService;
  }
  public async Task Reciver()
  {
    var model = _consumerService.GetRabbitModel(exchangeName: "Exchange Name",
    exchangeType: "direct", queueName: "Queue Name", routingKey: "Routing Key");
    var consumer = new AsyncEventingBasicConsumer(model);

    consumer.Received += async (ch, ea) =>
    {
      //Implement your business

      model.BasicAck(ea.DeliveryTag, false);

      await Task.CompletedTask;
    };
    model.BasicConsume(queue: "Queue Name", false, consumer);
  }
```



