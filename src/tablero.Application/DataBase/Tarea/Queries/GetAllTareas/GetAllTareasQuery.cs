using MongoDB.Driver;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Queries.GetAllTareas
{
    public class GetAllTareasQuery : IGetAllTareasQuery
    {
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public GetAllTareasQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _mongoDataBaseService = mongoDataBaseService;
        }

        public async Task<List<TareaEntity>> Execute()
        {
            List<TareaEntity> list = new();
            list = await _mongoDataBaseService.Tarea.Find(_ => true)
                .Project(e => new TareaEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero = e.IdTablero,
                    Titulo = e.Titulo,
                    IdTarea = e.IdTarea,
                    UserId = e.UserId
                }).ToListAsync();

            return list;
        }
    }
}
