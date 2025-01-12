using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;

namespace tablero.Application.DataBase.Tablero.Queries.GetTableroById
{
    public interface IGetTableroByIdQuery
    {
        Task<TableroEntity> Execute(int tableroId);
    }
}
