using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class DiscountCodeSearchByRules : SearchByRules<DiscountCode>
    {
        public DiscountCodeSearchByRules()
        {
            SearchByList = new List<string>() { "name" };
        }
        public DiscountCodeSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "name" };
            SearchByDictionary = new Dictionary<string, Expression<Func<DiscountCode, bool>>>()
            {
                    {"name",n=>searchValue!=""&&n.CodeName.ToLower().Contains(searchValue.ToLower()) },
            };
        }
    }
}
