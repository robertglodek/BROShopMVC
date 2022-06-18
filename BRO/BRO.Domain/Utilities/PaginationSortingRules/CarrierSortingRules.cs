using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class CarrierSortingRules : SortingRules<Carrier>
    {
        public CarrierSortingRules()
         : base(new List<string>() { "nazwa" }, new Dictionary<string, Expression<Func<Carrier, string>>>() 
         { { "nazwa", n => n.Name } })
        {
        }
    }
}
