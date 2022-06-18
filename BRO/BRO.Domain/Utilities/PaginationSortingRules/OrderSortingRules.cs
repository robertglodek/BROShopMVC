using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    
    public class OrderSortingRules : SortingRules<OrderHeader>
    {
        public Dictionary<string, Expression<Func<OrderHeader, DateTimeOffset?>>> SortDictionaryDate { get; private set; }
        public Dictionary<string, Expression<Func<OrderHeader, int>>> SortDictionaryNumber { get; private set; }
        public OrderSortingRules()
           : base(new List<string>() { "numer zamówienia", "data zakupu".ToLower() })
        {
            SortDictionaryDate = new Dictionary<string, Expression<Func<OrderHeader, DateTimeOffset?>>>()
            { { "data zakupu".ToLower(), n =>n.OrderDate }
            };

            SortDictionaryNumber = new Dictionary<string, Expression<Func<OrderHeader, int>>>()
            { { "numer zamówienia".ToLower(), n =>n.OrderNumber }
            };
        }
    }
}
