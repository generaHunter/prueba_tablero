using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.Commands.UpdateTablero;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Commands.UpdateTarea
{
    public class UpdateTareaCommand: IUpdateTareaCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        private readonly IMapper _mapper;

        public UpdateTareaCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
            _mapper = mapper;
        }

        public async Task<UpdateTareaModel> Execute(UpdateTareaModel model)
        {
            var entity = _mapper.Map<TareaEntity>(model);
            _dataBaseService.Tarea.Update(entity);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<TareaEntity>.Filter.Eq(e => e.IdTarea, model.IdTarea);
                var update = Builders<TareaEntity>.Update
                    .Set(e => e.Titulo, model.Titulo)
                    .Set(e => e.Descripcion, model.Descripcion)
                    .Set(e => e.IdEstado, model.IdEstado);
                await _mongoDataBaseService.Tarea.UpdateOneAsync(filter, update);
            }

            return model;

        }
    }
}
