using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Tarea.Commands.DefultModel
{
    public class DefaultTareaModel
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public int IdEstado { get; set; }
        public int IdTablero { get; set; }
        public int UserId { get; set; }
    }
}
