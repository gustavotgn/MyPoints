using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Message.Integration.Configurators;
using MyPoints.Message.Integration.Interfaces;
using MyPoints.Message.Integration.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace MyPoints.Message.Integration.Extensions
{
    public static class RabbitMQExtensions
    {
        public static RabbitConnections ConfigureRabbitMQConnection(this IServiceCollection services, Action<IServiceCollectionConfigurator> configure)
        {

            var configurator = new ServiceCollectionConfigurator(services);

            configure?.Invoke(configurator);

            return configurator.Configure();


        }
        public static RabbitConnections AddRabbitMQMessageService(this RabbitConnections connections)
        {
            connections.Services.AddSingleton<IMessageService>(new RabbitService(connections.Factory,connections.Connection,connections.Channel));
            
            return connections;

        }

        public static RabbitConnections AddRabbiMQConsumer<T, TCommandResult>(this RabbitConnections connections, string queue, bool saveOnError = true)
            where T : ICommand<TCommandResult>
            where TCommandResult : ICommandResult
        {

            connections.Channel.QueueDeclare(queue: queue,
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null);


            var consumer = new EventingBasicConsumer(connections.Channel);
            consumer.Received += async (sender, e) =>
            {
                var command = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(e.Body.ToArray()));
                
                var mediator = connections.Services.BuildServiceProvider().GetRequiredService<IMediator>();
                var result = await mediator.Send(command);
                if (!result.Succeeded && saveOnError)
                {
                    connections.Channel.QueueDeclare(queue: $"{queue}-error",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
                    connections.Channel.BasicPublish(exchange: "",
                                         routingKey: $"{queue}-error",
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { Message = command, Error = result.Errors })));
                }
            };

            connections.Channel.BasicConsume(queue: queue,
                autoAck: true,
                consumer: consumer);

            return connections;
        }

    }

}
