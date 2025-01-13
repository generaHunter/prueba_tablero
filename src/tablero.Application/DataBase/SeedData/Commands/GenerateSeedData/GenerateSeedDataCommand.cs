using AutoMapper;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.SeedData.Commands.GenerateSeedData
{
    public class GenerateSeedDataCommand: IGenerateSeedDataCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public GenerateSeedDataCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
        }

        public async Task<bool> Execute()
        {
            try
            {
                var tableros = new List<TableroEntity>();
                var random = new Random();
                var faker = new Bogus.Faker();

                //Generamos los tableros
                for (int i = 1; i < 10000; i++)
                {
                    tableros.Add(new TableroEntity
                    {
                        Nombre = faker.Lorem.Word(),
                        Descripcion = faker.Lorem.Sentence(),
                        FechaCreacion = faker.Date.Past().ToUniversalTime(),
                        UserId = 1,
                    });
                }
                await _dataBaseService.Tablero.AddRangeAsync(tableros);
                var result = await _dataBaseService.SaveAsync();

                //Replicamos datos en mongo
                if (result)
                {
                    await _mongoDataBaseService.Tablero.InsertManyAsync(tableros);
                }

                //Generamos las tareas
                var tareas = new List<TareaEntity>();
                for (int i = 1; i < 90000; i++)
                {
                    tareas.Add(new TareaEntity
                    {
                        Titulo = faker.Lorem.Word(),
                        Descripcion = faker.Lorem.Sentence(),
                        FechaCreacion = faker.Date.Past().ToUniversalTime(),
                        IdEstado = random.Next(1, 4),
                        IdTablero = random.Next(1, 10000),
                        UserId = 1
                    });
                }

                await _dataBaseService.Tarea.AddRangeAsync(tareas);
                var resultTareas = await _dataBaseService.SaveAsync();

                //Replicamos datos en mongo
                if (resultTareas)
                {
                    await Task.Delay(500);
                    await _mongoDataBaseService.Tarea.InsertManyAsync(tareas);
                }
            }
            catch (Exception ex)
            {

                return false;
            }


            return true;

        }
    }
}
