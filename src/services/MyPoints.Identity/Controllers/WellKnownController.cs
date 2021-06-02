using Microsoft.AspNetCore.Mvc;
using MyPoints.Identity.Configurations;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyPoints.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellKnownController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost("rabbit")]
        public async Task<IActionResult> Post(
           [FromServices] RabbitMQConfigurations configurations,
           [FromBody] ConteudoDto conteudo)
        {

            var factory = new ConnectionFactory() {
                HostName = configurations.HostName,
                Port = configurations.Port,
                UserName = configurations.UserName,
                Password = configurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "mypoints.rabbitmq-test",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message =
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - " +
                    $"Conteúdo da Mensagem: {conteudo.Mensagem}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "mypoints.rabbitmq-test",
                                     basicProperties: null,
                                     body: body);
            }

            return Ok(new {
                Resultado = "Mensagem encaminhada com sucesso"
            });

        }
    }

    //TODO remover após testes
    public class ConteudoDto
    {
        public string Mensagem { get; set; }
    }
}
