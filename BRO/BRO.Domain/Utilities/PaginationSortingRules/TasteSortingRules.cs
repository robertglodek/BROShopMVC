using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class TasteSortingRules:SortingRules<Taste>
    {
        public TasteSortingRules()
            :base(new List<string>() { "nazwa" }, new Dictionary<string, Expression<Func<Taste, string>>>() 
            { { "nazwa", n => n.Name } })
        {
        }
    }
}
