using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ForgotPasswordCommandValidator:AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(n => n.Email).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Email).EmailAddress().WithMessage("Nieprawidłowy format Email");
            RuleFor(n => n.Email).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
        }
    }
}
