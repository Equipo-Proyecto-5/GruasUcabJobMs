
using JobMs.Application.Queries;
using JobMs.Core.Services;
using MediatR;

namespace JobMs.Application.Handlers
{
    public class OrdenExpiradaHandler : IRequestHandler<OrdenExpiradaQuery>
    {
        private readonly IOrdersMsService OrdersMsService;

        public OrdenExpiradaHandler(IOrdersMsService ordersMsService)
        {
            OrdersMsService = ordersMsService;
        }

        public async Task Handle(OrdenExpiradaQuery request, CancellationToken cancellationToken)
        {
            await OrdersMsService.OrdenesExpiradas();
        }
    }
}
