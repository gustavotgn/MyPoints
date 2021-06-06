using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.CommandContract.Interfaces
{
    public interface ISyncIntegrationService
    {
        Task<T> SendAsync<T>(string queueName, dynamic conteudo, Method method = Method.POST, string token = null);
    }
}
