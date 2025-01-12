using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Tablero.Commands.CreateTablero;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Application.DataBase.Tablero.Queries.GetAllTableros;
using tablero.Application.DataBase.Tablero.Queries.GetTableroById;
using tablero.Application.DataBase.Tarea.Commands.CreateTarea;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;
using tablero.Application.DataBase.Tarea.Commands.DeleteTarea;
using tablero.Application.DataBase.Tarea.Commands.UpdateEstadoTarea;
using tablero.Application.DataBase.Tarea.Commands.UpdateTarea;
using tablero.Application.DataBase.Tarea.Queries.GetAllTareas;
using tablero.Application.DataBase.Tarea.Queries.GetTareaById;
using tablero.Application.DataBase.Tarea.Queries.GetTareasByTableroId;
using tablero.Application.Exceptions;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    [Authorize]
    [Route("api/v1/tarea")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class TareaController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(
    [FromBody] DefaultTareaModel model,
    [FromServices] ICreateTareaCommand command)
        {
            var data = await command.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(
    [FromServices] IGetAllTareasQuery query)
        {
            var data = await query.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(
            [FromQuery] int tareaId,
            [FromServices] IGetTareaByIdQuery query)
        {
            if (tareaId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, tareaId));

            }
            var data = await query.Execute(tareaId);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("getByTableroId")]
        public async Task<IActionResult> GetByTableroId(
            [FromQuery] int tableroId,
            [FromServices] IGetTareasByTableroId query)
        {
            if (tableroId == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, tableroId));

            }
            var data = await query.Execute(tableroId);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(
    [FromBody] UpdateTareaModel model,
    [FromServices] IUpdateTareaCommand command)
        {
            var data = await command.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpPatch("updateEstado")]
        public async Task<IActionResult> UpdateEstado
            (
            [FromQuery] int tareaId,
            [FromQuery] int estadoId,
            [FromServices] IUpdateTareaEstadoCommand command
            )
        {
            var data = await command.Execute(tareaId, estadoId);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] int tareaId,
            [FromServices] IDeleteTareaCommand command)
        {
            var data = await command.Execute(tareaId);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
