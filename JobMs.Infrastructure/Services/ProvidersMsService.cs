using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobMs.Core.Services;
using Microsoft.Extensions.Configuration;

namespace JobMs.Infrastructure.Services
{
    public class ProvidersMsService : IProvidersMsService
    {
        private readonly HttpClient HttpClient;
        private readonly IConfiguration Config;

        public ProvidersMsService(HttpClient httpClient, IConfiguration config)
        {
            HttpClient = httpClient;
            Config = config;
        }
        public async Task LocalizacionGruaExpirada()
        {
            var response = await HttpClient.GetAsync(Config["ServiosUrl:MsProviderVencido"]);
            var result = response;
        }
    }
}
