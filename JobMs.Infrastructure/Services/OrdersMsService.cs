
using JobMs.Core.Services;
using Microsoft.Extensions.Configuration;

namespace JobMs.Infrastructure.Services
{
    public class OrdersMsService : IOrdersMsService
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;

        public OrdersMsService(HttpClient httpClient, IConfiguration config)
        {
            HttpClient = httpClient;
            Config = config;
        }
        public async Task OrdenesExpiradas()
        {
            var response = await HttpClient.GetAsync(Config["ServiosUrl:MsOrderVencida"]);
            var result = response;
        }
    }
}
