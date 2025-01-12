using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tablero.Domain.Entities.Estado
{
    public class EstadoEntity
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public ICollection<TareaEntity>? Tareas { get; set; }
    }
}
