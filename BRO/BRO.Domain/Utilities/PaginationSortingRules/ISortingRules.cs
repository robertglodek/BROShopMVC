using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public interface ISortingRules<T>
    {
        List<string> SortList { get;}
        Dictionary<string, Expression<Func<T, string>>> SortDictionary { get; }
    }
}
