using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Usuario.Commands.CreateUsuarioSeed
{
    public interface ICreateUsuarioSeedCommand
    {
        Task<bool> Execute();
    }
}
