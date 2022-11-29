using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Core;

namespace Consumer3_delayed
{
    internal class ExchangeConsumer3
    {
        private const string _queueName = "demo-direct-delayed-queue_3-ruano-test";

        public static void Consume(IModel channel)
        {
            Dictionary<string, object> args = new() { { "x-delayed-type", "direct" } };
            channel.ExchangeDeclare(exchange: RabbitMqConstants.DelayedExchangeName, type: "x-delayed-message", durable: true, autoDelete: false, args);
            channel.QueueDeclare(_queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

            channel.QueueBind(_queueName, RabbitMqConstants.DelayedExchangeName, RabbitMqConstants.RoutingKey);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(_queueName, true, consumer);
            Console.WriteLine("Consumer_3 started");
            Console.ReadLine();
        }
    }
}
