using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ConfirmEmailCommandValidator:AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {         
            RuleFor(n => n.token).NotEmpty().WithMessage("Token nie może być pusty");
            RuleFor(n => n.Email).NotEmpty().WithMessage("Email nie może być pusty");
        }
    }
}
