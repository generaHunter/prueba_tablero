using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;

namespace tablero.Application.DataBase.Estado.Queries.GetAllEstadosQuery
{
    public interface IGetAllEstadosQuery
    {
        Task<List<EstadoEntity>> Execute();
    }
}
