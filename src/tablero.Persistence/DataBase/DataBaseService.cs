using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase;
using tablero.Domain.Entities.Estado;
using tablero.Domain.Entities.Tablero;
using tablero.Domain.Entities.Tarea;
using tablero.Domain.Entities.Usuario;
using tablero.Persistence.Configuration;

namespace tablero.Persistence.DataBase
{
    public class DataBaseService: DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EstadoEntity> Estado { get; set; }
        public DbSet<TableroEntity> Tablero { get; set; }
        public DbSet<TareaEntity> Tarea { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntitiesConfiguration(modelBuilder);
        }

        private void EntitiesConfiguration(ModelBuilder modelBuilder)
        {
            new UsuarioConfiguration(modelBuilder.Entity<UsuarioEntity>());
            new EstadoConfiguration(modelBuilder.Entity<EstadoEntity>());
            new TableroConfiguration(modelBuilder.Entity<TableroEntity>());
            new TareaConfiguration(modelBuilder.Entity<TareaEntity>());
        }
    }
}
