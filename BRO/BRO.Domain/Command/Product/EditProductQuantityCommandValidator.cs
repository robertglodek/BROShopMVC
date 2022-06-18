using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ProductTaste
{
    public class EditProductQuantityCommandValidator : AbstractValidator<EditProductQuantityCommand>
    {
        public EditProductQuantityCommandValidator()
        {
            RuleForEach(n => n.ProductTastes).ChildRules(n => n.RuleFor(n => n.InStock).InclusiveBetween(0, 100000).WithMessage("Niepoprawna liczba: przedział 0 - 100000"));
        }
    }
}
