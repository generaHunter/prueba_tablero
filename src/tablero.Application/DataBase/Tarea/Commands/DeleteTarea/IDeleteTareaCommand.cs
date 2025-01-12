using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Tarea.Commands.DeleteTarea
{
    public interface IDeleteTareaCommand
    {
        Task<bool> Execute(int idTarea);
    }
}
