using BRO.Domain.Command.Review;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {
            RuleFor(n => n.Rating).InclusiveBetween(1, 5).WithMessage("Niepoprawna liczba: przedział 1 - 5");
            
        }
    }
}