
using JobMs.Core.Services;
using Microsoft.Extensions.Configuration;

namespace JobMs.Infrastructure.Services
{
    public class UsersMsService : IUserMsService
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;

        public UsersMsService(HttpClient httpClient, IConfiguration config)
        {
            HttpClient = httpClient;
            Config = config;
        }

        public async  Task ProcesarEnvioNotificacion()
        {
            var response = await HttpClient.PostAsync(Config["ServiosUrl:MsUserEnviaNotificacion"],null);
            var result = response;
        }
    }
}

