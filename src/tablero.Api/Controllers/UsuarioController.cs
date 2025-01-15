using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tablero.Application.DataBase.Estado.Commands.CreateEstadoSeed;
using tablero.Application.DataBase.Usuario.Commands.CreateUsuarioSeed;
using tablero.Application.DataBase.Usuario.Queries.GetUserByCredentials;
using tablero.Application.Exceptions;
using tablero.Application.External.JWT;
using tablero.Application.Features;

namespace tablero.Api.Controllers
{
    [Authorize]
    [Route("api/v1/usuario")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UsuarioController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet("getUserByCredential/{userName}/{userPassword}")]
        public async Task<IActionResult> GetUserByCredential(
      string userName,
      string userPassword,
[FromServices] IGetUserByCredentialsQuery query,
[FromServices] IValidator<(string userName, string userPassword)> validator,
[FromServices] IGetTokenJWTService _getTokenService)
        {
            var validate = await validator.ValidateAsync((userName, userPassword));

            if (!validate.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            }

            //if (userName.IsNullOrEmpty() || userPassword.IsNullOrEmpty())
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));
            //}

            var data = await query.Execute(userName, userPassword);

            if (data == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));
            }

            data.Token = _getTokenService.GetAccessToken(data.UserId.ToString());

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [AllowAnonymous]
        [HttpGet("createUsuarioSeed")]
        public async Task<IActionResult> CreateUsuarioSeed(
[FromServices] ICreateUsuarioSeedCommand command)
        {
            var data = await command.Execute();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data ? "Usuario admin Creado" : "Usuario admin ya existen"));
        }
    }
}
