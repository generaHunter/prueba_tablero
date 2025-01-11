using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Domain.Entities.Estado
{
    public class EstadoEntity
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }

        public ICollection<TareaEntity>? Tareas { get; set; }
    }
}
