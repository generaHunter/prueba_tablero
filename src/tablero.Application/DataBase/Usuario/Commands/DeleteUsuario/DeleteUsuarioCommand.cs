using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using tablero.Domain.Entities.Usuario;

namespace tablero.Application.DataBase.Usuario.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommand : IDeleteUsuarioCommand
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMongoDataBaseService _mongoDataBaseService;
        public DeleteUsuarioCommand(IDataBaseService dataBaseService, IMongoDataBaseService mongoDataBaseService)
        {
            _dataBaseService = dataBaseService;
            _mongoDataBaseService = mongoDataBaseService;
        }

        public async Task<bool> Execute(int idUsuario)
        {
            var entity = await _dataBaseService.Usuario.FirstOrDefaultAsync(x => x.UserId == idUsuario);
            if (entity == null) { return false; }

            _dataBaseService.Usuario.Remove(entity);

            var result = await _dataBaseService.SaveAsync();
            //Replicamos datos en mongo
            if (result)
            {
                var filter = Builders<UsuarioEntity>.Filter.Eq(e => e.UserId, idUsuario);
                var resultMongo = await _mongoDataBaseService.Usuario.DeleteOneAsync(filter);
                result = resultMongo.DeletedCount > 0;
            }

            return result;
        }
    }
}
