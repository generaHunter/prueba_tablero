using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Estado.Commands.CreateEstadoSeed
{
    public interface ICreateEstadoSeedCommand
    {
        Task<bool> Execute();
    }
}
