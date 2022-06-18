using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class TasteSearchByRules : SearchByRules<Taste>
    {
        public TasteSearchByRules()
        {
            SearchByList = new List<string>() { "name" };
        }
        public TasteSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "name" };
            SearchByDictionary = new Dictionary<string, Expression<Func<Taste, bool>>>()
            {
               {"name",n=>searchValue!="" && n.Name.ToLower().Contains(searchValue.ToLower()) },
            };
        }
    }
}
