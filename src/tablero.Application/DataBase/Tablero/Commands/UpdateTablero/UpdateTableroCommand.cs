using AutoMapper;
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

        public async Task<UpdateTableroModel> Execute(UpdateTableroModel model)
        {
            var entity = _mapper.Map<TableroEntity>(model);
            _dataBaseService.Tablero.Update(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TableroEntity>.Filter.Eq(e => e.IdTablero, model.IdTablero);
                var update = Builders<TableroEntity>.Update
                    .Set(e => e.Nombre, model.Nombre)
                    .Set(e => e.Descripcion, model.Descripcion);
                await _mongoDataBaseService.Tablero.UpdateOneAsync(filter, update);
            }

            return model;

        }
    }
}
