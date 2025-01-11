using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tarea;

namespace tablero.Persistence.Configuration
{
    public class TareaConfiguration
    {
        public TareaConfiguration(EntityTypeBuilder<TareaEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdTarea);
            entityBuilder.Property(x => x.Descripcion).IsRequired();
            entityBuilder.Property(x => x.FechaCreacion).IsRequired();

            // Configure foreign key relationship to EstadoEntity
            entityBuilder.HasOne(b => b.Estado)
                .WithMany(b => b.Tareas)
                .HasForeignKey(b => b.IdEstado);

            // Configure foreign key relationship to TableroEntity
            entityBuilder.HasOne(b => b.Tablero)
                .WithMany(b => b.Tareas)
                .HasForeignKey(b => b.IdTablero);

            entityBuilder.HasOne(b => b.Usuario).WithMany(b => b.Tareas).HasForeignKey(b => b.UserId);
        }
    }
}
