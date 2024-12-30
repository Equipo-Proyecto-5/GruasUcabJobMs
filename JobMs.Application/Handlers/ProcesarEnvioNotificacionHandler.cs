using JobMs.Application.Queries;
using JobMs.Core.Services;
using MediatR;

namespace JobMs.Application.Handlers
{
    public class ProcesarEnvioNotificacionHandler : IRequestHandler<ProcesarEnvioNotificacionQuery>
    {
        private readonly IUserMsService UserMsService;

        public ProcesarEnvioNotificacionHandler(IUserMsService userMsService)
        {
           UserMsService = userMsService;
        }

        public async Task Handle(ProcesarEnvioNotificacionQuery request, CancellationToken cancellationToken)
        {
            await UserMsService.ProcesarEnvioNotificacion();
        }
    }
}
