using MongoDB.Driver;
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

namespace tablero.Persistence.DataBase
{
    public class MongoDataBaseService: IMongoDataBaseService
    {
        private readonly IMongoDatabase _database;

        public MongoDataBaseService(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<EstadoEntity> Estado => _database.GetCollection<EstadoEntity>("Estado");
        public IMongoCollection<TableroEntity> Tablero => _database.GetCollection<TableroEntity>("Tablero");
        public IMongoCollection<TareaEntity> Tarea => _database.GetCollection<TareaEntity>("Tarea");
        public IMongoCollection<UsuarioEntity> Usuario => _database.GetCollection<UsuarioEntity>("Usuario");

    }
}
