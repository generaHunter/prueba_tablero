using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.Dtos
{
    public class ReporteTareasDto
    {
        public int IdTablero { get; set; }
        public string Tablero { get; set; }
        public int TotalTareas { get; set; }
        public int Pendiente { get; set; }
        public int EnProgreso { get; set; }
        public int Completada { get; set; }
        public double PorcentajeCompletadas { get; set; }
    }
}
