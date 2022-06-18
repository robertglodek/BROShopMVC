using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class OrderSearchByRules : SearchByRules<OrderHeader>
    {
        public OrderSearchByRules()
        {
            SearchByList = new List<string>() { "number" };
        }
        public OrderSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "number" };
            SearchByDictionary = new Dictionary<string, Expression<Func<OrderHeader, bool>>>()
            {
                    {("number").ToLower(),n=>searchValue!=""&&n.OrderNumber.ToString().Contains(searchValue.ToLower()) },
            };
        }
    }
}
