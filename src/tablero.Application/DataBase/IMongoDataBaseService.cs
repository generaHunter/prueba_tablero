using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.DataBase
{
    public interface IMongoDataBaseService
    {
        IMongoCollection<EstadoEntity> Estado { get; }
        IMongoCollection<TableroEntity> Tablero { get; }
        IMongoCollection<TareaEntity> Tarea { get; }
        IMongoCollection<UsuarioEntity> Usuario { get; }
    }
}
