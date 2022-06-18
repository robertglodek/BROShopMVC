using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ProductTaste
{
    class GetProductTastesQueryValidator:AbstractValidator<GetProductTastesQuery>
    {
        public GetProductTastesQueryValidator()
        {
            
        }
    }
}
