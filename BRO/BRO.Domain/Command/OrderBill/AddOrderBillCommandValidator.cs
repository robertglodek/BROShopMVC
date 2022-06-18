using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderBill
{
    public class AddOrderBillCommandValidator:AbstractValidator<AddOrderBillCommand>
    {
        public AddOrderBillCommandValidator()
        {
            RuleFor(n => n.CompanyName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.CompanyName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");

            RuleFor(n => n.NIP).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.NIP).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");

            RuleFor(n => n.Street).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Street).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");

            RuleFor(n => n.City).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.City).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");

            RuleFor(n => n.PostalCode).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.PostalCode).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PostalCode).Matches("[0-9]{2}-[0-9]{3}").WithMessage("Nieprawidłowy format: xx-xxx");
        }
    }
}
