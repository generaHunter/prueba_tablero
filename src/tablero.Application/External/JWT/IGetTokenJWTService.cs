using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.External.JWT
{
    public interface IGetTokenJWTService
    {
        string GetAccessToken(string id);
    }
}
