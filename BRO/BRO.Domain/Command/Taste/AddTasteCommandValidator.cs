using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Taste
{
    public class AddTasteCommandValidator:AbstractValidator<AddTasteCommand>
    {
        public AddTasteCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Name).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
        }
    }
}
