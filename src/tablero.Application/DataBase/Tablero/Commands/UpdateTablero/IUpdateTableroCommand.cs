using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Tablero.Commands.UpdateTablero
{
    public interface IUpdateTableroCommand
    {
        Task<UpdateTableroModel> Execute(UpdateTableroModel model);
    }
}
