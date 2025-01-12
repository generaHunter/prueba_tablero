using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Queries.GetTareasByTableroId
{
    public class GetTareasByTableroId: IGetTareasByTableroId
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public GetTareasByTableroId(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<List<TareaEntity>> Execute(int tableroId)
        {
            List<TareaEntity> list = new();

            var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTablero, tableroId);

            list = await _dataBaseService.Tarea.Find(filter)
                .Project(e => new TareaEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero = e.IdTablero,
                    IdTarea = e.IdTarea,
                    Titulo = e.Titulo,
                    IdEstado = e.IdEstado,
                    UserId = e.UserId
                }).ToListAsync();

            return list;
        }
    }
}

