using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.Dtos;

namespace tablero.Application.DataBase.Reportes.ReporteTareas
{
    public interface IReporteTareasQuery
    {
        Task<List<ReporteTareasDto>> Execute();
    }
}
