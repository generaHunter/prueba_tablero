using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Tablero.Commands.CreateTablero;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Application.DataBase.Tablero.Queries.GetAllTableros;
using tablero.Application.DataBase.Tablero.Queries.GetTableroById;
using tablero.Application.Exceptions;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    [Authorize]
    [Route("api/v1/tablero")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class TableroController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] DefaultTableroModel model,
            [FromServices] ICreateTableroCommand command)
        {
            var data = await command.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(
    [FromServices] IGetAllTablerosQuery query)
        {
            var data = await query.Execute();
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(
            [FromQuery] int tableroId,
            [FromServices] IGetTableroByIdQuery query)
        {
            if (tableroId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, tableroId));

            }
            var data = await query.Execute(tableroId);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(
    [FromBody] UpdateTableroModel model,
    [FromServices] IUpdateTableroCommand command)
        {
            var data = await command.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
