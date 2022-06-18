using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LoginApplicationUserCommandValidator:AbstractValidator<LoginApplicationUserCommand>
    {
        public LoginApplicationUserCommandValidator()
        {
            RuleFor(n => n.Email).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Email).EmailAddress().WithMessage("Nieprawidłowy format Email"); 
            RuleFor(n => n.Password).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
        }
    }
}
