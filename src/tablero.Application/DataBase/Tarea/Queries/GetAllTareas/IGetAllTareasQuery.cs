using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Queries.GetAllTareas
{
    public interface IGetAllTareasQuery
    {
        Task<List<TareaEntity>> Execute();
    }
}
