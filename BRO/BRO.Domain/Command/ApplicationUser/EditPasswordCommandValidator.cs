using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
   
    class EditPasswordCommandValidator : AbstractValidator<EditPasswordCommand>
    {
        public EditPasswordCommandValidator()
        {
            RuleFor(n => n.Password).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.Password).MinimumLength(8).WithMessage("Minimalna liczba znaków: 8");
            RuleFor(n => n.Password).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n => n.Password).Matches("\\d").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę");
            RuleFor(n => n.ConfirmPassword).Equal(s => s.Password).WithMessage("Podane hasła różnią się");
            RuleFor(n => n.ConfirmPassword).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.ConfirmPassword).MinimumLength(6).WithMessage("Minimalna liczba znaków: 8");
            RuleFor(n => n.ConfirmPassword).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n => n.PasswordToConfirm).NotEmpty().WithMessage("Zawartość kolumny nie może być pusta");
            RuleFor(n => n.PasswordToConfirm).MinimumLength(8).WithMessage("Minimalna liczba znaków: 8");
            RuleFor(n => n.PasswordToConfirm).MaximumLength(50).WithMessage("Maksymalna liczba znaków: 50");
            RuleFor(n => n.PasswordToConfirm).Matches("\\d").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę");
        }

    }
}
