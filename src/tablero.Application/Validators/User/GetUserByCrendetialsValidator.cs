using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tablero.Application.Validators.User
{
    public class GetUserByCrendetialsValidator : AbstractValidator<(string userName, string userPassword)>
    {
        public GetUserByCrendetialsValidator()
        {
            RuleFor(x => x.userName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .Must(NoTengaCaracteresPeligrosos).WithMessage("La entrada contiene caracteres no permitidos.").OverridePropertyName("userName");
            RuleFor(x => x.userPassword)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10)
                .Must(NoTengaCaracteresPeligrosos).WithMessage("La entrada contiene caracteres no permitidos.").OverridePropertyName("userPassword"); ;
        }

        private bool NoTengaCaracteresPeligrosos(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return true;

            // Lista de caracteres peligrosos comunes en inyecciones SQL
            string[] caracteresPeligrosos = { "'", "\"", ";", "--", "/*", "*/", "xp_" };

            foreach (var caracter in caracteresPeligrosos)
            {
                if (input.Contains(caracter)) return false;
            }

            return true;
        }
    }
}
