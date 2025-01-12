using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Commands.UpdateTarea
{
    public class UpdateTareaCommand : IUpdateTareaCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public UpdateTareaCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;

        }

        public async Task<bool> Execute(UpdateTareaModel model)
        {
            var entity = await _dataBaseService.Tarea.FirstOrDefaultAsync(x => x.IdTarea == model.IdTarea);

            if (entity == null)
            {
                return false;
            }

            entity.Titulo = model.Titulo;
            entity.Descripcion = model.Descripcion;
            entity.FechaCreacion = entity.FechaCreacion.ToUniversalTime();
            entity.IdEstado = model.IdEstado;

            _dataBaseService.Tarea.Update(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTarea, model.IdTarea);
                var update = Builders<TareaEntity>.Update
                    .Set(e => e.Titulo, model.Titulo)
                    .Set(e => e.Descripcion, model.Descripcion)
                    .Set(e => e.IdEstado, model.IdEstado);
                await _mongoDataBaseService.Tarea.UpdateOneAsync(filter, update);
            }

            return true;
        }
    }
}
