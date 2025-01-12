using Microsoft.EntityFrameworkCore;
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
    public interface IDataBaseService
    {
        DbSet<EstadoEntity> Estado { get; set; }
        DbSet<TableroEntity> Tablero { get; set; }
        DbSet<TareaEntity> Tarea { get; set; }
        DbSet<UsuarioEntity> Usuario { get; set; }

        Task<bool> SaveAsync();
    }
}
