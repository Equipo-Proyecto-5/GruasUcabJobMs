using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobMs.Application.Queries;
using JobMs.Core.Services;
using MediatR;

namespace JobMs.Application.Handlers
{
    public class GruaExpiradaQueryHandler : IRequestHandler<GruaExpiradaQuery>
    {
        private readonly IProvidersMsService ProvidersMsService;

        public GruaExpiradaQueryHandler(IProvidersMsService providersMsService)
        {
            ProvidersMsService = providersMsService;
        }
        public async Task Handle(GruaExpiradaQuery request, CancellationToken cancellationToken)
        {
            await ProvidersMsService.LocalizacionGruaExpirada();

        }
    }
}
