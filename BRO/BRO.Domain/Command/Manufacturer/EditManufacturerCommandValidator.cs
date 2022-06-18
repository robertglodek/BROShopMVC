using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Manufacturer
{
    public class EditManufacturerCommandValidator : AbstractValidator<EditManufacturerCommand>
    {
        public EditManufacturerCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Name).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
        }
    }
}
