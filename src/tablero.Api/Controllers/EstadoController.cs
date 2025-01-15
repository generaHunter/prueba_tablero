using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Estado.Commands.CreateEstadoSeed;
using tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery;
using tablero.Application.DataBase.SeedData.Commands.GenerateSeedData;
using tablero.Application.DataBase.Tablero.Queries.GetAllTableros;
using tablero.Application.Exceptions;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    /// <summary>
    /// EstadoController
    /// </summary>
    [Authorize]
    [Route("api/v1/estado")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class EstadoController : ControllerBase
    {

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(
[FromServices] IGetAllEstadosQuery query)
        {
            var data = await query.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [AllowAnonymous]
        [HttpGet("generateEstadosSeed")]
        public async Task<IActionResult> GenerateEstadosSeed(
[FromServices] ICreateEstadoSeedCommand command)
        {
            var data = await command.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data ? "Estados Creado" : "Estados ya existen"));
        }
    }
}
