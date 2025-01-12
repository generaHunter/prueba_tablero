using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;
using tablero.Domain.Entities.Estado;

namespace tablero.Application.DataBase.Estado.Commands.CreateEstado
{
    public class CreateEstadoCommand: ICreateEstadoCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public CreateEstadoCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<DefaultEstadoModel> Execute(DefaultEstadoModel model)
        {
            var entity = _mapper.Map<EstadoEntity>(model);
            await _dataBaseService.Estado.AddAsync(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
              await _mongoDataBaseService.Estado.InsertOneAsync(entity);
            }

            return model;

        }
    }
}
