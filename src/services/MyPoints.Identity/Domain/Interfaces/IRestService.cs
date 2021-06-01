using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Interfaces
{
    public interface IRestService
    {
        Task<T> SendAsync<T>(string queueName, dynamic conteudo, Method method = Method.POST, string token = null);
        Task SendAsync(string queueName, dynamic conteudo, Method method = Method.POST, string token = null);
    }
}
