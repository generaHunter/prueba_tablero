using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Commands.UpdateTablero
{
    public class UpdateTableroCommand: IUpdateTableroCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public UpdateTableroCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<bool> Execute(UpdateTableroModel model)
        {
                var entity = await _dataBaseService.Tablero.FirstOrDefaultAsync(x => x.IdTablero == model.IdTablero);

                if (entity == null)
                {
                    return false;
                }

                entity.Nombre = model.Nombre;
                entity.Descripcion = model.Descripcion;
                entity.FechaCreacion = entity.FechaCreacion.ToUniversalTime();

                _dataBaseService.Tablero.Update(entity);
                var result = await _dataBaseService.SaveAsync();

                //Replicamos datos en mongo
                if (result)
                {
                    var filter = Builders<TableroEntity>.Filter.Eq(e => e.IdTablero, model.IdTablero);
                    var update = Builders<TableroEntity>.Update
                        .Set(e => e.Nombre, entity.Nombre)
                        .Set(e => e.Descripcion, entity.Descripcion);
                    await _mongoDataBaseService.Tablero.UpdateOneAsync(filter, update);
                }

            return true;

        }
    }
}
