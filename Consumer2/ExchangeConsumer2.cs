using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Core;

namespace Consumer2
{
    internal class ExchangeConsumer2
    {
        private const string _queueName = "demo-direct-queue_2-ruano-test";

        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(exchange: RabbitMqConstants.ExchangeName, type: ExchangeType.Direct);
            channel.QueueDeclare(_queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

            channel.QueueBind(_queueName, RabbitMqConstants.ExchangeName, RabbitMqConstants.RoutingKey);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(_queueName, true, consumer);
            Console.WriteLine("Consumer_2 started");
            Console.ReadLine();
        }
    }
}
