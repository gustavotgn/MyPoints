using MyPoints.CommandContract.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MyPoints.Message.Integration.Services
{
    public class RabbitService : IMessageService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        public RabbitService(ConnectionFactory factory, IConnection conn, IModel channel)
        {
            _factory = factory;
            _conn = conn;
            _channel = channel;
        }

        public bool Enqueue<T>(string queueName, T obj)
        {
            _channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var message = JsonConvert.SerializeObject(obj);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            return true;
        }
    }
}
