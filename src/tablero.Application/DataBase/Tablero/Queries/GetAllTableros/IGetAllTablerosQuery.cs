using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Queries.GetAllTableros
{
    public interface IGetAllTablerosQuery
    {
        Task<List<TableroEntity>> Execute();
    }
}
