using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tablero.Application.External.JWT;

namespace tablero.External.GetTokenJWT
{
    public class GetTokenJWTService : IGetTokenJWTService
    {
        private readonly IConfiguration _configuration;
        public GetTokenJWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAccessToken(string id)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            string key = _configuration.GetSection("JWT:SecretKeyJWT").Value ?? string.Empty;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration.GetSection("JWT:IssuerJWT").Value ?? string.Empty,
                Audience = _configuration.GetSection("JWT:AudienceJWT").Value ?? string.Empty
            };

            var token = tokenHandle.CreateToken(tokenDescriptor);
            var tokenString = tokenHandle.WriteToken(token);
            return tokenString;
        }
    }
}
