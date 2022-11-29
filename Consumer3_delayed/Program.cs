using Consumer3_delayed;
using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
ExchangeConsumer3.Consume(channel);