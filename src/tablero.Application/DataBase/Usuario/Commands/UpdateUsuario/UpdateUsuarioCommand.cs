using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.DataBase.Usuario.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommand: IUpdateUsuarioCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;
        public UpdateUsuarioCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<UpdateUsuarioModel> Execute(UpdateUsuarioModel model)
        {
            var entity = _mapper.Map<UsuarioEntity>(model);
            _dataBaseService.Usuario.Update(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<UsuarioEntity>.Filter.Eq(e => e.UserId, model.UserId);
                var update = Builders<UsuarioEntity>.Update
                    .Set(e => e.FirstName, model.FirstName)
                    .Set(e => e.LastName, model.LastName)
                    .Set(e => e.UserName, model.UserName)
                    .Set(e => e.Password, model.Password);
                await _mongoDataBaseService.Usuario.UpdateOneAsync(filter, update);
            }

            return model;

        }
    }
}
