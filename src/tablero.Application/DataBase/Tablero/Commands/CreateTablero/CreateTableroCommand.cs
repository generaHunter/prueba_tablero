using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Commands.CreateTablero
{
    public class CreateTableroCommand : ICreateTableroCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public CreateTableroCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<DefaultTableroModel> Execute(DefaultTableroModel model)
        {
            try
            {
                var entity = _mapper.Map<TableroEntity>(model);
                await _dataBaseService.Tablero.AddAsync(entity);
                var result = await _dataBaseService.SaveAsync();

                //Replicamos datos en mongo
                if (result)
                {
                    await _mongoDataBaseService.Tablero.InsertOneAsync(entity);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return model;

        }
    }
}
