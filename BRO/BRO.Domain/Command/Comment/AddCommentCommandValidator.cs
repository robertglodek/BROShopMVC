using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class AddCommentCommandValidator:AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(n => n.Content).MaximumLength(100).WithMessage("Maksymalna liczba znaków: 100");
            RuleFor(n => n.Content).MinimumLength(5).WithMessage("Minimalna liczba znaków: 5");
        }
    }
}
