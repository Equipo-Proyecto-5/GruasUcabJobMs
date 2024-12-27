using JobMs.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GruasUcabJobMs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator Mediator;

        public JobsController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpGet("/ordenExpirada")]
        public async Task<IActionResult> OrdenesExpiradas()
        {
            try
            {
                var query = new OrdenExpiradaQuery();
                await Mediator.Send(query);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "Hubo un error al procesar la busqueda");
            }
        }
    }
}
