using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class ApplicationUserSortingRules : SortingRules<ApplicationUser>
    {
        public ApplicationUserSortingRules()
         : base(new List<string>() {"email","imię"}, new Dictionary<string, Expression<Func<ApplicationUser, string>>>() 
         { {"email".ToLower(), n => n.Email } ,
           {"imię".ToLower(), n => n.FirstName }})
        {
        }
    }
}
