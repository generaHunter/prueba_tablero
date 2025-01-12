using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Usuario.Queries.GetUserByCredentials
{
    public interface IGetUserByCredentialsQuery
    {
        Task<GetUserByCredentialsModel> Execute(string userName, string password);
    }
}
