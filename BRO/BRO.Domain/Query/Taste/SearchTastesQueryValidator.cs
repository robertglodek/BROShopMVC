using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities.StaticDetails;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Taste
{
    public class SearchTastesQueryValidator : AbstractValidator<SearchTastesQuery>
    {
        private IEnumerable<int> allowedPageSizes = PagedResultSizes.GetAllowedSizes();
        public SearchTastesQueryValidator()
        {
            RuleFor(n => n).Custom((value, context) =>
            {
                if (value.PageNumber < 1)
                    context.AddFailure("PageNumber", "Page number has to be greater than 0");
                if (!allowedPageSizes.Contains(value.PageSize))
                    context.AddFailure("PageSize", $"The page size must be included in  {string.Join("/", allowedPageSizes)}");
            });
        }
    }
}
