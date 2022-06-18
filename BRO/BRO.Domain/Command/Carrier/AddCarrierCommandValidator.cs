using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Carrier
{
    public class AddCarrierCommandValidator : AbstractValidator<AddCarrierCommand>
    {
        public AddCarrierCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Name).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");      
            RuleFor(n => n.DeliveryTimeScope).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.DeliveryTimeScope).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n => n.FreeShippingFromPrice).InclusiveBetween(10,100000).WithMessage("Niepoprawna liczba: przedział 10 - 100000");
            RuleFor(n => n.ShippingCost).InclusiveBetween(5, 100000).WithMessage("Niepoprawna liczba: przedział 5 - 100000");
            RuleFor(n => n.Image).MaximumLength(100).WithMessage("Maksymalna liczba znaków pliku: 100");
            RuleFor(n => n).Custom((value, context) =>
            {
                if (value.Image != null)
                {
                    if (!value.Image.Contains("png") && !value.Image.Contains("jpg"))
                        context.AddFailure("Image", "Akceptowane formaty pliku: png, jpg");
                }
            });
        }
    }
}
