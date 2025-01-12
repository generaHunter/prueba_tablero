using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;
using tablero.Application.DataBase.Usuario.DefaultModel;
using tablero.Domain.Entities.Tarea;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.DataBase.Usuario.Commands.CreateUsuario
{
    public class CreateUsuarioCommand: ICreateUsuarioCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public CreateUsuarioCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<DefaultUsuarioModel> Execute(DefaultUsuarioModel model)
        {
            var entity = _mapper.Map<UsuarioEntity>(model);
            await _dataBaseService.Usuario.AddAsync(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                model.UserId = entity.UserId;
                await _mongoDataBaseService.Usuario.InsertOneAsync(entity);
            }

            return model;

        }
    }
}
