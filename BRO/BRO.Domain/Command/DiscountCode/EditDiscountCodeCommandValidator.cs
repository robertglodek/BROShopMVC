using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.DiscountCode
{
   
    public class EditDiscountCodeCommandValidator : AbstractValidator<EditDiscountCodeCommand>
    {
        public EditDiscountCodeCommandValidator()
        {
            RuleFor(n => n.CodeName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.CodeName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.CodeName).MinimumLength(3).WithMessage("Minimalna liczba znaków: 3");
            RuleFor(n => n.NumberOfUsesLeft).InclusiveBetween(0, 100000).WithMessage("Niepoprawna liczba: przedział 0 - 100000");
            RuleFor(n => n.MinimumOrderPrice).InclusiveBetween(0, 100000).WithMessage("Niepoprawna liczba: przedział 0 - 100000");
            RuleFor(n => n.CodeAvailabilityEnd).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.DiscountInPercent).InclusiveBetween(1, 80).WithMessage("Niepoprawna liczba: przedział 1 - 80");
        }
    }
}
