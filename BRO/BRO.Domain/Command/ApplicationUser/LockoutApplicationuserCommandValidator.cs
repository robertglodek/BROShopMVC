using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LockoutApplicationuserCommandValidator:AbstractValidator<LockoutApplicationuserCommand>
    {
        public LockoutApplicationuserCommandValidator()
        {       
            RuleFor(n => n.LockoutReason).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 100");
        }
    }
}
