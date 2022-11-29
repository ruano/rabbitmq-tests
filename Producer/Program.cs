using Producer;
using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",

};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
ExchangePublisher.Publish(channel);