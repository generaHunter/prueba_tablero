using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;
using tablero.Domain.Entities.Estado;

namespace tablero.Application.DataBase.Estado.Commands.CreateEstadoSeed
{
    public class CreateEstadoSeedCommand: ICreateEstadoSeedCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public CreateEstadoSeedCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
        }

        public async Task<bool> Execute()
        {
            var estados = await _dataBaseService.Estado.ToListAsync();
            if (estados.Count == 0)
            {
                List<EstadoEntity> estadosNew = new List<EstadoEntity>();

                EstadoEntity estadoPendiente = new()
                {
                    NombreEstado = "Pendiente"
                };

                EstadoEntity estadoEnProgreso = new()
                {
                    NombreEstado = "En progreso"
                };

                EstadoEntity estadoCompletada = new()
                {
                    NombreEstado = "Completada"
                };

                estadosNew.Add(estadoPendiente);
                estadosNew.Add(estadoEnProgreso);
                estadosNew.Add(estadoCompletada);

                await _dataBaseService.Estado.AddRangeAsync(estadosNew);


                var result = await _dataBaseService.SaveAsync();

                //Replicamos datos en mongo
                if (result)
                {
                    await _mongoDataBaseService.Estado.InsertManyAsync(estadosNew);
                    return true;
                }
            }


            return false;

        }
    }
}
