using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;

namespace tablero.Domain.Entities.Usuario
{
    public class UsuarioEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<TableroEntity> Tableros { get; set; }
        public ICollection<TareaEntity> Tareas { get; set; }
    }
}
