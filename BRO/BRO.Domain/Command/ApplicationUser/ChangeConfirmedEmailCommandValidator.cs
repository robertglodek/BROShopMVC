using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    
    class ChangeConfirmedEmailCommandValidator : AbstractValidator<ChangeConfirmedEmailCommand>
    {
        public ChangeConfirmedEmailCommandValidator()
        {
            RuleFor(n => n.NewEmail).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.NewEmail).EmailAddress().WithMessage("Nieprawidłowy format Email");
            RuleFor(n => n.Token).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta"); 
        }
    }
}
