using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;

namespace tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery
{
    public class GetAllEstadosQuery: IGetAllEstadosQuery
    {
        private readonly IMongoDataBaseService _dataBaseService;

        public GetAllEstadosQuery(IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = mongoDataBaseService;
        }

        public async Task<List<EstadoEntity>> Execute()
        {
            List<EstadoEntity> estadosList = new();
            estadosList = await _dataBaseService.Estado.Find(_ => true)
                .Project(e => new EstadoEntity
                {
                    IdEstado = e.IdEstado,
                    NombreEstado = e.NombreEstado,
                }).ToListAsync();

            return estadosList;
        }
    }
}
