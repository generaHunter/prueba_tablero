using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Usuario.DefaultModel;

namespace tablero.Application.DataBase.Usuario.Commands.CreateUsuario
{
    public interface ICreateUsuarioCommand
    {
        Task<DefaultUsuarioModel> Execute(DefaultUsuarioModel model);
    }
}
