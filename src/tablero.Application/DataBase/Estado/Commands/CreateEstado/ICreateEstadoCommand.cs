using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Estado.Commands.DefaultModel;

namespace tablero.Application.DataBase.Estado.Commands.CreateEstado
{
    public interface ICreateEstadoCommand
    {
        Task<DefaultEstadoModel> Execute(DefaultEstadoModel model);
    }
}
