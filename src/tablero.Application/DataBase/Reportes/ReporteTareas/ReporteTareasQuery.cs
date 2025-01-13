using MongoDB.Bson;
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
            // Obtener los datos necesarios de las colecciones
            var tareas = await _dataBaseService.Tarea.Find(_ => true)
                .Project(e => new TareaEntity
                {
                    IdEstado = e.IdEstado,
                    IdTablero = e.IdTablero
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
                    IdTablero = e.IdTablero,
                    Nombre = e.Nombre
                })
                .ToListAsync();

            // Generar el reporte agrupando por tablero
            var reporte = tareas
                .GroupBy(t => t.IdTablero)
                .Select(grupo =>
                {
                    var tableroId = grupo.Key;
                    var tableroNombre = tableros.FirstOrDefault(t => t.IdTablero == tableroId)?.Nombre ?? "Desconocido";

                    var totalTareas = grupo.Count();
                    var pendiente = grupo.Count(t => estados.FirstOrDefault(e => e.IdEstado == t.IdEstado)?.NombreEstado == "Pendiente");
                    var enProgreso = grupo.Count(t => estados.FirstOrDefault(e => e.IdEstado == t.IdEstado)?.NombreEstado == "En progreso");
                    var completada = grupo.Count(t => estados.FirstOrDefault(e => e.IdEstado == t.IdEstado)?.NombreEstado == "Completada");
                    var porcentajeCompletadas = totalTareas > 0 ? (double)completada / totalTareas * 100 : 0;

                    return new ReporteTareasDto
                    {
                        IdTablero = tableroId,
                        Tablero = tableroNombre,
                        TotalTareas = totalTareas,
                        Pendiente = pendiente,
                        EnProgreso = enProgreso,
                        Completada = completada,
                        PorcentajeCompletadas = Math.Round(porcentajeCompletadas, 2)
                    };
                })
                .ToList();

            return reporte;
        }
    }
}
