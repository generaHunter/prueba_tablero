using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
            services.AddDbContext<DataBaseService>(options => options.UseNpgsql(configuration.GetSection("ConnectionStrings:PostgressConnectionString").Value));
            services.AddScoped<IDataBaseService, DataBaseService>();

            //Mongo
            services.AddSingleton<IMongoClient>(sp => new MongoClient(configuration.GetSection("ConnectionStrings:MongoDbConnectionString").Value));
            services.AddSingleton<IMongoDataBaseService>(sp =>
                new MongoDataBaseService(sp.GetRequiredService<IMongoClient>(), "tablero_database"));

            return services;
        }

    }
}
