using Microsoft.Extensions.DependencyInjection;
using MyPoints.Message.Integration.Extensions;
using MyPoints.Message.Integration.Interfaces;
using MyPoints.Message.Integration.Services;
using MyPoints.Message.Integration.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Message.Integration.Configurators
{
    public class ServiceCollectionConfigurator : IServiceCollectionConfigurator
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        private IServiceCollection _services;
        public ServiceCollectionConfigurator(IServiceCollection services)
        {
            _services = services;
        }


        internal RabbitConnections Configure()
        {
            ConnectionFactory _factory;
            IConnection _conn;
            IModel _channel;


            _factory = new ConnectionFactory() {
                HostName = HostName,
                Port = Port,
                UserName = UserName,
                Password = Password
            };
            int count = 0;
            while (true)
            {
                count++;
                try
                {
                    _conn = _factory.CreateConnection();
                    break;
                }
                catch (Exception ex)
                {
                    if (count > 40)
                    {
                        throw ex;
                    }
                    Task.Delay(2000).Wait();
                }
            }
            _channel = _conn.CreateModel();

            return new RabbitConnections(_factory, _conn, _channel, _services);

        }
    }
}
