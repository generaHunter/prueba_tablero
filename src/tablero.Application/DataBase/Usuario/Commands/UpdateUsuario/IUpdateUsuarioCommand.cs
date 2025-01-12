using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Usuario.Commands.UpdateUsuario
{
    public interface IUpdateUsuarioCommand
    {
        Task<UpdateUsuarioModel> Execute(UpdateUsuarioModel model);
    }
}
