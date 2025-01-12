using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Commands.DeleteTarea
{
    public class DeleteTareaCommand: IDeleteTareaCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;


        public DeleteTareaCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;

        }

        public async Task<bool> Execute(int idTarea)
        {

            var entity = await _dataBaseService.Tarea.FirstOrDefaultAsync(x => x.IdTablero == idTarea);
            if (entity == null) { return false; }

            _dataBaseService.Tarea.Remove(entity);

            var result = await _dataBaseService.SaveAsync();
            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTarea, idTarea);
                var resultMongo = await _mongoDataBaseService.Tarea.DeleteOneAsync(filter);
                result = resultMongo.DeletedCount > 0;
            }

            return result;

        }
    }
}
