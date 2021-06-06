using Microsoft.AspNetCore.Http;
using MyPoints.CommandContract.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Message.Integration.Services
{
    public class RestService : ISyncIntegrationService
    {
        private IHttpContextAccessor _http;


        public RestService(IHttpContextAccessor httpContextAccessor)
        {
            _http = httpContextAccessor;

        }

        public async Task<T> SendAsync<T>(string queueName, dynamic conteudo, Method method = Method.POST, string token = null)
        {
            if (!token?.StartsWith("Bearer") == true)
            {
                token = "Bearer " + token;
            }
            var client = new RestClient(queueName);
            var request = new RestRequest(method);

            var tokenHeader = _http.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value;
            request.AddHeader("Content-Type", "application/json");

            if (!String.IsNullOrWhiteSpace(tokenHeader.ToString() + token))
            {
                request.AddHeader("Authorization", token ?? tokenHeader.ToString());
            }
            if (method != Method.GET)
            {
                var body = JsonConvert.SerializeObject(conteudo);
                request.AddParameter("application/json", body, ParameterType.RequestBody);

            }

            IRestResponse response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
