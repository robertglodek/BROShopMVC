using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    class EditShoppingCartItemCommandValidator : AbstractValidator<EditShoppingCartItemCommand>
    {
        public EditShoppingCartItemCommandValidator()
        {
            RuleFor(n => n.ShoppingCartItemId).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Count).GreaterThan(0).WithMessage("Minimalna ilość: 1");
           



        }
    }
}
