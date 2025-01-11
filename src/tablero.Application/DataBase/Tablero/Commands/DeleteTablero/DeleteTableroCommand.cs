using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Commands.DeleteTablero
{
    public class DeleteTableroCommand : IDeleteTableroCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;


        public DeleteTableroCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;

        }

        public async Task<bool> Execute(int idTablero)
        {

            var entity = await _dataBaseService.Tablero.FirstOrDefaultAsync(x => x.IdTablero == idTablero);
            if (entity == null) { return false; }

            _dataBaseService.Tablero.Remove(entity);

            var result = await _dataBaseService.SaveAsync();
            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TableroEntity>.Filter.Eq(e => e.IdTablero, idTablero);
                var resultMongo = await _mongoDataBaseService.Tablero.DeleteOneAsync(filter);
                result = resultMongo.DeletedCount > 0;
            }

            return result;

        }
    }
}
