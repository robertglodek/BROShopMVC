using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class EditApplicationUserCommandValidator : AbstractValidator<EditApplicationUserCommand>
    {
        public EditApplicationUserCommandValidator()
        {
            RuleFor(n => n.FirstName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.FirstName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.LastName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PhoneNumber).Matches("[0-9]{9}").WithMessage("Nieprawidłowy format: xxxxxxxxx");
            RuleFor(n => n.Street).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.City).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PostalCode).Matches("[0-9]{2}-[0-9]{3}").WithMessage("Nieprawidłowy format: xx-xxx");
        }
    }
}
