using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class CategorySearchByRules : SearchByRules<Category>
    {
        public CategorySearchByRules()
        {
            SearchByList = new List<string>() { "name" };
        }
        public CategorySearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "name" };
            SearchByDictionary = new Dictionary<string, Expression<Func<Category, bool>>>()
            {
                    {"name",n=>searchValue!=""&&n.Name.ToLower().Contains(searchValue.ToLower()) },
            };
        }
    }
}
