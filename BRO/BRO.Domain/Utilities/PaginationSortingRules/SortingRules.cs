using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class SortingRules<T> : ISortingRules<T>
    {
        public List<string> SortList { get; private set; }
        public Dictionary<string, Expression<Func<T, string>>> SortDictionary { get; private set; }
        public SortingRules()
        {
        }
        public SortingRules(List<string> sortList)
        {
            SortList = sortList;
        }
        public SortingRules(List<string> sortList, Dictionary<string, Expression<Func<T, string>>> sortDictionary)
        {
            SortList = sortList;
            SortDictionary = sortDictionary;
        }
    }
}
