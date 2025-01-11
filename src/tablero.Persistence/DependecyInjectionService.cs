using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tablero.Application.DataBase;
using tablero.Persistence.DataBase;

namespace tablero.Persistence
{
    public static class DependecyInjectionService
    {
        public static IServiceCollection AddPersitence(this IServiceCollection services, IConfiguration configuration)
        {
            var t = configuration.GetSection("ConnectionStrings:PostgressConnectionString").Value;
            services.AddDbContext<DataBaseService>(options => options.UseNpgsql(configuration.GetSection("ConnectionStrings:PostgressConnectionString").Value));

            services.AddScoped<IDataBaseService, DataBaseService>();

            return services;
        }

    }
}
