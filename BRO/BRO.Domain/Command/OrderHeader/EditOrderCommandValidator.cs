using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderHeader
{
    public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(n => n.FirstName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.FirstName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.LastName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.LastName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PostalCode).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.PostalCode).Matches("[0-9]{2}-[0-9]{3}").WithMessage("Nieprawidłowy format: xx-xxx");
            RuleFor(n => n.Street).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Street).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.City).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.City).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PhoneNumber).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.PhoneNumber).Matches("[0-9]{9}").WithMessage("Nieprawidłowy format: xxxxxxxxx");
            RuleFor(n => n.MessageToUser).MaximumLength(200).WithMessage("Maksymalna liczba znaków: 200");
            RuleFor(n => n.TrackingNumber).MaximumLength(100).WithMessage("Maksymalna liczba znaków: 100");
        }
    }
}
