using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Tablero;

namespace tablero.Persistence.Configuration
{
    public class TableroConfiguration
    {
        public TableroConfiguration(EntityTypeBuilder<TableroEntity> entityBuilder)
        {
            entityBuilder.ToTable("tablero");
            entityBuilder.HasKey(x => x.IdTablero);
            entityBuilder.Property(x => x.IdTablero).HasColumnName("idtablero");

            entityBuilder.Property(x => x.Nombre).IsRequired().HasColumnName("nombre");
            entityBuilder.Property(x => x.Descripcion).IsRequired().HasColumnName("descripcion");
            entityBuilder.Property(x => x.FechaCreacion).IsRequired().HasColumnName("fechacreacion");
            entityBuilder.Property(x => x.UserId).IsRequired().HasColumnName("userid");

            entityBuilder.HasMany(x => x.Tareas).WithOne(x => x.Tablero).HasForeignKey(x => x.IdTablero);
            entityBuilder.HasOne(b => b.Usuario).WithMany(b => b.Tableros).HasForeignKey(b => b.UserId);
        }
    }
}
