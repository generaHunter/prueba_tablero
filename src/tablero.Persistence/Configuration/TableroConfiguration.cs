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
            entityBuilder.HasKey(x => x.IdTablero);
            entityBuilder.Property(x => x.Nombre).IsRequired();
            entityBuilder.Property(x => x.Descripcion).IsRequired();
            entityBuilder.Property(x => x.FechaCreacion).IsRequired();

            entityBuilder.HasMany(x => x.Tareas).WithOne(x => x.Tablero).HasForeignKey(x => x.IdTablero);
        }
    }
}
