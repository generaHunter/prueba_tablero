using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tablero.DefaultModel;

namespace tablero.Application.DataBase.Tablero.Commands.CreateTablero
{
    public interface ICreateTableroCommand
    {
        Task<DefaultTableroModel> Execute(DefaultTableroModel model);
    }
}
