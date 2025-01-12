using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Commands.CreateTarea
{
    public class CreateTareaCommand: ICreateTareaCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public CreateTareaCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<DefaultTareaModel> Execute(DefaultTareaModel model)
        {
            var entity = _mapper.Map<TareaEntity>(model);
            await _dataBaseService.Tarea.AddAsync(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                model.IdTarea = entity.IdTarea;
                await _mongoDataBaseService.Tarea.InsertOneAsync(entity);
            }

            return model;

        }
    }
}
