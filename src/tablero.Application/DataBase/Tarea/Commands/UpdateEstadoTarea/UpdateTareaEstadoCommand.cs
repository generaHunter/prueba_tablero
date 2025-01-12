using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tarea.Commands.UpdateTarea;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Commands.UpdateEstadoTarea
{
    public class UpdateTareaEstadoCommand: IUpdateTareaEstadoCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public UpdateTareaEstadoCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;

        }

        public async Task<bool> Execute(int idTarea, int idEstado)
        {
            var entity = await _dataBaseService.Tarea.FirstOrDefaultAsync(x => x.IdTarea == idTarea);

            if (entity == null)
            {
                return false;
            }

            entity.FechaCreacion = entity.FechaCreacion.ToUniversalTime();
            entity.IdEstado = idEstado;

            _dataBaseService.Tarea.Update(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTarea, idTarea);
                var update = Builders<TareaEntity>.Update
                    .Set(e => e.IdEstado, idEstado);
                await _mongoDataBaseService.Tarea.UpdateOneAsync(filter, update);
            }

            return true;
        }
    }
}
