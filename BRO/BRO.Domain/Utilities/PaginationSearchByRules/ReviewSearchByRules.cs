using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    
    public class ReviewSearchByRules : SearchByRules<Review>
    {
        public ReviewSearchByRules()
        {
            SearchByList = new List<string>() { "product" };
        }
        public ReviewSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "product" };
            SearchByDictionary = new Dictionary<string, Expression<Func<Review, bool>>>()
            {
                    {"product",n=>searchValue!=""&&n.Product.Name.Contains(searchValue.ToLower()) },
            };
        }
    }
}
