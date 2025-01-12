using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Queries.GetAllTableros
{
    public class GetAllTablerosQuery: IGetAllTablerosQuery
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public GetAllTablerosQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<List<TableroEntity>> Execute()
        {
            List<TableroEntity> list = new();
            list = await _dataBaseService.Tablero.Find(_ => true)
                .Project(e => new TableroEntity
                {
                    Descripcion = e.Descripcion,
                    FechaCreacion = e.FechaCreacion,
                    IdTablero = e.IdTablero,
                    Nombre = e.Nombre,
                    UserId = e.UserId
                }).ToListAsync();

            return list;
        }
    }
}
