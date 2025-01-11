using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Domain.Entities.Tablero
{
    public class TableroEntity
    {
        public int IdTablero { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

        public ICollection<TareaEntity>? Tareas{ get; set; }
    }
}
