using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class SearchByRules<T> : ISearchByRules<T>
    {
        public List<string> SearchByList { get; protected set; }
        public Dictionary<string, Expression<Func<T, bool>>> SearchByDictionary { get; protected set; }
    }
}
