using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    class AddShoppingCartItemCommandValidator : AbstractValidator<AddShoppingCartItemCommand>
    {
        public AddShoppingCartItemCommandValidator()
        {
            RuleFor(n => n.ProductTasteId).NotEmpty().WithMessage("Należy wybrać smak produktu");
            RuleFor(n => n.Count).GreaterThan(0).WithMessage("Minimalna ilość: 1"); 
        }
    }
}
