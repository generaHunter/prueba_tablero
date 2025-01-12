using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Reportes.ReporteTareas;
using tablero.Application.DataBase.Tablero.Queries.GetAllTableros;
using tablero.Application.Exceptions;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    [Authorize]
    [Route("api/v1/reporte")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class ReporteController : ControllerBase
    {

        [HttpGet("getReporteTareas")]
        public async Task<IActionResult> GetReporteTareas(
[FromServices] IReporteTareasQuery query)
        {
            var data = await query.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
