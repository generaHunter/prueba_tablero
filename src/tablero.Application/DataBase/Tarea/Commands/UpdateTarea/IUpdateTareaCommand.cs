using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Tarea.Commands.UpdateTarea
{
    public interface IUpdateTareaCommand
    {
        Task<bool> Execute(UpdateTareaModel model);
    }
}
