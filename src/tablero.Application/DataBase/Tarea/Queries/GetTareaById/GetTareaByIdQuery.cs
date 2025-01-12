using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Queries.GetTareaById
{
    public class GetTareaByIdQuery: IGetTareaByIdQuery
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public GetTareaByIdQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<TareaEntity> Execute(int tareaId)
        {
            TareaEntity item = new();

            var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTarea, tareaId);

            item = await _dataBaseService.Tarea.Find(filter)
                .Project(e => new TareaEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero = e.IdTablero,
                    IdTarea = e.IdTarea,
                    Titulo = e.Titulo,
                    IdEstado = e.IdEstado,
                    UserId = e.UserId
                }).FirstOrDefaultAsync();

            return item;
        }
    }
}
