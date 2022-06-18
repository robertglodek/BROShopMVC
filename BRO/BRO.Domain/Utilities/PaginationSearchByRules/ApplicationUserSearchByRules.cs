using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class ApplicationUserSearchByRules : SearchByRules<ApplicationUser>
    {
        public ApplicationUserSearchByRules()
        {
            SearchByList = new List<string>() { "email" };
        }
        public ApplicationUserSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "email" };
            SearchByDictionary = new Dictionary<string, Expression<Func<ApplicationUser, bool>>>()
            {
                    {"email",n=>searchValue!=""&&n.Email.ToLower().Contains(searchValue.ToLower()) },
            };

        }
    }
}
