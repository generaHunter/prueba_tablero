using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;

namespace tablero.Domain.Entities.Tarea
{
    public class TareaEntity
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public int IdTablero { get; set; }
        public TableroEntity? Tablero { get; set; }
        public EstadoEntity? Estado { get; set; }
    }
}
