using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Application.DataBase.Tarea.Queries.GetTareaById
{
    public interface IGetTareaByIdQuery
    {
        Task<TareaEntity> Execute(int tareaId);
    }
}
