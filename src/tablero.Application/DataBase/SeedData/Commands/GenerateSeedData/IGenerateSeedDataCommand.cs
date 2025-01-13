using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.SeedData.Commands.GenerateSeedData
{
    public interface IGenerateSeedDataCommand
    {
        Task<bool> Execute();
    }
}
