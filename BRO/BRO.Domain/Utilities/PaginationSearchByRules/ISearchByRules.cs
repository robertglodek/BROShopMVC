using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public interface ISearchByRules<T> 
    {
        List<string> SearchByList { get; }
        Dictionary<string, Expression<Func<T, bool>>> SearchByDictionary { get; }
    }
}
