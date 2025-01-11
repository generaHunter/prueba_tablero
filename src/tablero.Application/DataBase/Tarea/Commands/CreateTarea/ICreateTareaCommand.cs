using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;

namespace tablero.Application.DataBase.Tarea.Commands.CreateTarea
{
    public interface ICreateTareaCommand
    {
        Task<DefaultTareaModel> Execute(DefaultTareaModel model);
    }
}
