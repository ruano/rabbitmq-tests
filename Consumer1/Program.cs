using Consumer1;
using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
ExchangeConsumer1.Consume(channel);