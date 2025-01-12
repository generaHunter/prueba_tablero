using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Domain.Entities.Usuario;

namespace tablero.Persistence.Configuration
{
    public class UsuarioConfiguration
    {
        public UsuarioConfiguration(EntityTypeBuilder<UsuarioEntity> entityBuilder)
        {
            entityBuilder.ToTable("usuario");
            entityBuilder.HasKey(x => x.UserId);
            entityBuilder.Property(x => x.UserId).HasColumnName("userid");
            entityBuilder.Property(x => x.FirstName).IsRequired().HasColumnName("firstname");
            entityBuilder.Property(x => x.LastName).IsRequired().HasColumnName("lastname"); 
            entityBuilder.Property(x => x.UserName).IsRequired().HasColumnName("username");
            entityBuilder.Property(x => x.Password).IsRequired().HasColumnName("password");

            entityBuilder.HasMany(x => x.Tableros).WithOne(x => x.Usuario).HasForeignKey(x => x.UserId);
            entityBuilder.HasMany(x => x.Tareas).WithOne(x => x.Usuario).HasForeignKey(x => x.UserId);
        }
    }
}
