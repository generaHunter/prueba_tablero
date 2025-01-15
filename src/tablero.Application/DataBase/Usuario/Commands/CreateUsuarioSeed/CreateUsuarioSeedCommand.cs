using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Usuario.DefaultModel;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.DataBase.Usuario.Commands.CreateUsuarioSeed
{
    public class CreateUsuarioSeedCommand: ICreateUsuarioSeedCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;

        public CreateUsuarioSeedCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
        }

        public async Task<bool> Execute()
        {
            var usuarioAdmin = await _dataBaseService.Usuario.FirstOrDefaultAsync(x => x.UserName == "admin");

            if (usuarioAdmin != null) return false;

            UsuarioEntity usuario = new()
            {
                FirstName = "admin",
                LastName = "admin",
                Password = "admin",
                UserName = "admin"
            };
            await _dataBaseService.Usuario.AddAsync(usuario);
            var result = await _dataBaseService.SaveAsync();

            //Replicamos datos en mongo
            if (result)
            {
                await _mongoDataBaseService.Usuario.InsertOneAsync(usuario);
            }

            return true;

        }
    }
}
