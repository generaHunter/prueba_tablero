using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Estado;

namespace tablero.Persistence.Configuration
{
    public class EstadoConfiguration
    {
        public EstadoConfiguration(EntityTypeBuilder<EstadoEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdEstado);
            entityBuilder.Property(x => x.NombreEstado).IsRequired();

            entityBuilder.HasMany(x => x.Tareas).WithOne(x => x.Estado).HasForeignKey(x => x.IdEstado);

        }
    }
}
