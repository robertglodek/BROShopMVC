using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class DiscountCodeSortingRules : SortingRules<DiscountCode>
    {
        public Dictionary<string, Expression<Func<DiscountCode, int>>> SortDictionaryNumber { get; private set; }
        public DiscountCodeSortingRules()
           : base(new List<string>() { "nazwa", "zniżka" }, new Dictionary<string, Expression<Func<DiscountCode, string>>>()
            {{ "nazwa", n => n.CodeName }})
        {
            SortDictionaryNumber = new Dictionary<string, Expression<Func<DiscountCode, int>>>()
            { { "zniżka", n => n.DiscountInPercent}
            };
        }
    }
}
