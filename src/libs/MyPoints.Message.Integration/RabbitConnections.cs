using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MyPoints.Message.Integration
{
    public class RabbitConnections
    {
        public IServiceCollection Services { get; private set; }
        public ConnectionFactory Factory { get; private set; }
        public IConnection Connection { get; private set; }
        public IModel Channel { get; private set; }

        public RabbitConnections(ConnectionFactory factory, IConnection connection, IModel channel, IServiceCollection services)
        {
            Factory = factory;
            Connection = connection;
            Channel = channel;
            Services = services;
        }


    }

}
