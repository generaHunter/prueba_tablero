using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.Dtos;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Reportes.ReporteTareas
{
    public class ReporteTareasQuery: IReporteTareasQuery
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public ReporteTareasQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<List<ReporteTareasDto>> Execute()
        {
            List<ReporteTareasDto> list = new();

            // Obtener todos los datos de las colecciones necesarias
            var tareas = await _dataBaseService.Tarea.Find(_ => true)
                .Project(e => new TareaEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdEstado = e.IdEstado,
                    IdTablero = e.IdTablero,
                    IdTarea = e.IdTarea,
                    UserId = e.UserId
                })
                .ToListAsync();
            var estados = await _dataBaseService.Estado.Find(_ => true)
                .Project(e => new EstadoEntity
                {
                    IdEstado = e.IdEstado,
                    NombreEstado = e.NombreEstado
                })
                .ToListAsync();
            var tableros = await _dataBaseService.Tablero.Find(_ => true)
                .Project(e => new TableroEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero= e.IdTablero,
                    UserId= e.UserId,
                    Nombre = e.Nombre
                })
                .ToListAsync();

            // Generar el reporte agrupando las tareas por estado y tablero
            list = tareas
                .GroupBy(t => new { t.IdEstado, t.IdTablero })
                .Select(grupo =>
                {
                    var estado = estados.FirstOrDefault(e => e.IdEstado == grupo.Key.IdEstado)?.NombreEstado ?? "Desconocido";
                    var tablero = tableros.FirstOrDefault(t => t.IdTablero == grupo.Key.IdTablero)?.Nombre ?? "Desconocido";

                    var totalTareas = grupo.Count();
                    var completadas = grupo.Count(t => estados.FirstOrDefault(e => e.IdEstado == t.IdEstado)?.NombreEstado == "Completada");
                    var proporcionCompletadas = totalTareas > 0 ? (double)completadas / totalTareas : 0;

                    return new ReporteTareasDto
                    {
                        Estado = estado,
                        Tablero = tablero,
                        TotalTareas = totalTareas,
                        TareasCompletadas = completadas,
                        ProporcionCompletadas = proporcionCompletadas
                    };
                })
                .ToList();


            return list;
        }
    }
}
