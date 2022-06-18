using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    
    public class ReviewSortingRules : SortingRules<Review>
    {
        public Dictionary<string, Expression<Func<Review, DateTimeOffset?>>> SortDictionaryDate { get; private set; }
        public Dictionary<string, Expression<Func<Review, int>>> SortDictionaryNumber { get; private set; }
        public ReviewSortingRules()
            : base(new List<string>() { "data publikacji", "ocena".ToLower() })
        {
            SortDictionaryDate = new Dictionary<string, Expression<Func<Review, DateTimeOffset?>>>()
            { { "data publikacji".ToLower(), n =>n.PublishDateTime }
            };
            SortDictionaryNumber = new Dictionary<string, Expression<Func<Review, int>>>()
            { { "ocena", n => n.Rating }
            };
        }
    }
}
