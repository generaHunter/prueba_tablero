using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Queries.GetTableroById
{
    public class GetTableroByIdQuery: IGetTableroByIdQuery
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public GetTableroByIdQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<TableroEntity> Execute(int tableroId)
        {
            TableroEntity item = new();

            var filter = Builders<TableroEntity>.Filter.Eq(e => e.IdTablero, tableroId);

            item = await _dataBaseService.Tablero.Find(filter)
                .Project(e => new TableroEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero = e.IdTablero,
                    Nombre = e.Nombre,
                    UserId = e.UserId
                }).FirstOrDefaultAsync();

            return item;
        }
    }
}

