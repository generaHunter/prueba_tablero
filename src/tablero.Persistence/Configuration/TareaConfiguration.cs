using Microsoft.EntityFrameworkCore;
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
            entityBuilder.ToTable("tarea");
            entityBuilder.HasKey(x => x.IdTarea);
            entityBuilder.Property(x => x.IdTarea).HasColumnName("idtarea");
            entityBuilder.Property(x => x.Descripcion).IsRequired().HasColumnName("descripcion");
            entityBuilder.Property(x => x.FechaCreacion).IsRequired().HasColumnName("fechacreacion");
            entityBuilder.Property(x => x.Titulo).IsRequired().HasColumnName("titulo");
            entityBuilder.Property(x => x.UserId).HasColumnName("userid");
            entityBuilder.Property(x => x.IdEstado).IsRequired().HasColumnName("idestado");
            entityBuilder.Property(x => x.IdTablero).IsRequired().HasColumnName("idtablero");

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
