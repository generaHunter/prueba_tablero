using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery;
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
    }
}
