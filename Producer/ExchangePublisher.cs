using Core;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer
{
    public class ExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            //---------------- Multiple subscribers

            //var ttl = new Dictionary<string, object>
            //{
            //    { "x-message-ttl", 30000 }
            //};
            //channel.ExchangeDeclare(RabbitMqConstants.ExchangeName, ExchangeType.Direct, arguments: ttl);

            //var message = new { Name = "Producer", Message = $"Hello! Rabbit!" };
            //var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            //channel.BasicPublish(RabbitMqConstants.ExchangeName, RabbitMqConstants.RoutingKey, null, body);

            //---------------- Delayed exchange using rabbitmq-delayed-message-exchange plugin

            Dictionary<string, object> args = new() { { "x-delayed-type", "direct" } };
            channel.ExchangeDeclare(RabbitMqConstants.DelayedExchangeName, type: "x-delayed-message", durable: true, autoDelete: false, args);
                        
            Dictionary<string, object> headers = new() { { "x-delay", 60 * 1000 } };
            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = headers;

            var message = new { Name = "Producer", Message = $"Hello! Rabbit!" };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));            
            
            channel.BasicPublish(RabbitMqConstants.DelayedExchangeName, RabbitMqConstants.RoutingKey, basicProperties, body);
        }
    }
}
