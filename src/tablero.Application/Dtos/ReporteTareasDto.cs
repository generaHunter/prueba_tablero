using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.Dtos
{
    public class ReporteTareasDto
    {
        public string Estado { get; set; }
        public string Tablero { get; set; }
        public int TotalTareas { get; set; }
        public int TareasCompletadas { get; set; }
        public double ProporcionCompletadas { get; set; }
    }
}
