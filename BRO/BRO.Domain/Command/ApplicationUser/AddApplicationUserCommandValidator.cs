using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    /// <summary>
    /// Class responsible for validation,validates the form input
    /// </summary>
    class AddApplicationUserCommandValidator : AbstractValidator<AddApplicationUserCommand>
    {
        public AddApplicationUserCommandValidator()
        {
            RuleFor(n => n.Email).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Email).EmailAddress().WithMessage("Nieprawidłowy format Email");
            RuleFor(n => n.Email).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");


            RuleFor(n => n.FirstName).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.FirstName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.LastName).MaximumLength(30).WithMessage("Maksymalna liczba znaków: 30");
            RuleFor(n => n.PhoneNumber).Matches("[0-9]{9}").WithMessage("Nieprawidłowy format: xxxxxxxxx");
            RuleFor(n=>n.Password).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n=>n.Password).MinimumLength(8).WithMessage("Minimalna liczba znaków: 8");
            RuleFor(n=>n.Password).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n=>n.Password).Matches("\\d").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę");
            RuleFor(n => n.ConfirmPassword).Equal(s => s.Password).WithMessage("Podane hasła różnią się");
            RuleFor(n => n.ConfirmPassword).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.ConfirmPassword).MinimumLength(6).WithMessage("Minimalna liczba znaków: 8");
            RuleFor(n => n.ConfirmPassword).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
        } 
    }
}
