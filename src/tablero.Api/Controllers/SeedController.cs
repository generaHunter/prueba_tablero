using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery;
using tablero.Application.DataBase.SeedData.Commands.GenerateSeedData;
using tablero.Application.Exceptions;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    [Route("api/v1/seed")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class SeedController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("generateSeedData")]
        public async Task<IActionResult> GenerateSeedData(
[FromServices] IGenerateSeedDataCommand command)
        {
            var data = await command.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
