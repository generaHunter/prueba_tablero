using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.DataBase.Usuario.Queries.GetUserByCredentials
{
    public class GetUserByCredentialsQuery : IGetUserByCredentialsQuery
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IMapper _mapper;
        public GetUserByCredentialsQuery(IDataBaseService dataBaseService, IMapper mapper)
        {
            _dataBaseService = dataBaseService;
            _mapper = mapper;
        }

        public async Task<GetUserByCredentialsModel> Execute(string userName, string password)
        {
            var user = await _dataBaseService.Usuario.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            user.Password = string.Empty;
            return _mapper.Map<GetUserByCredentialsModel>(user);
        }
    }
}
