using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Product
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Name).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n => n.Portions).InclusiveBetween(1, 1000).WithMessage("Niepoprawna liczba: przedział 10 - 1000");
            RuleFor(n => n.Use).MaximumLength(300).WithMessage("Maksymalna liczba znaków: 300");
            RuleFor(n => n.NutritionalInformation).MaximumLength(300).WithMessage("Maksymalna liczba znaków: 300");
            RuleFor(n => n.Description).MaximumLength(300).WithMessage("Maksymalna liczba znaków: 300");
            RuleFor(n => n.WeightGram).InclusiveBetween(1, 100000).WithMessage("Niepoprawna liczba: przedział 1 - 100000");
            RuleFor(n => n.RegularPrice).InclusiveBetween(2, 1000).WithMessage("Niepoprawna liczba: przedział 2 - 100000");
            RuleFor(n => n.PromotionalPrice).InclusiveBetween(2, 1000).WithMessage("Niepoprawna liczba: przedział 2 - 100000");
            RuleFor(n => n.CategoryId).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.ManufacturerId).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");     
            RuleFor(n => n.ImageUrlLarge).MaximumLength(100).WithMessage("Maksymalna liczba znaków pliku: 100");
            RuleFor(n => n.ImageUrlMain).MaximumLength(100).WithMessage("Maksymalna liczba znaków pliku: 100");
            RuleFor(n => n).Custom((value, context) =>
            {
                if (!value.ImageUrlMain.Contains("png") && !value.ImageUrlMain.Contains("jpg"))
                    context.AddFailure("ImageUrlMain", "Akceptowane formaty pliku: png, jpg");
                if (!value.ImageUrlLarge.Contains("png") && !value.ImageUrlMain.Contains("jpg"))
                    context.AddFailure("ImageUrlLarge", "Akceptowane formaty pliku: png, jpg");
                if (value.TasteIds == null || value.TasteIds.Count() < 1)
                    context.AddFailure("TasteIds", "Produkt musi posiadać przynajmniej jeden smak");
            });

        }
    }
}
