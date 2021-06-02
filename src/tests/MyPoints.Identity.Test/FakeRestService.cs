using MyPoints.Identity.Domain.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test
{
    public class FakeRestService : IRestService
    {
        public async Task<T> SendAsync<T>(string queueName, dynamic conteudo, Method method = Method.POST, string token = null)
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public async Task SendAsync(string queueName, dynamic conteudo, Method method = Method.POST, string token = null)
        {
            return;
        }
    }
}
