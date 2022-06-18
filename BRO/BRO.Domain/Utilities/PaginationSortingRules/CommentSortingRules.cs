using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
   
    public class CommentSortingRules : SortingRules<Comment>
    {
        public Dictionary<string, Expression<Func<Comment, DateTimeOffset>>> SortDictionaryDate { get; private set; }
        public Dictionary<string, Expression<Func<Comment, bool>>> SortDictionaryBool { get; private set; }
        public CommentSortingRules()
            : base(new List<string>() { "data publikacji", "zaakceptowane" })
        {
            SortDictionaryDate = new Dictionary<string, Expression<Func<Comment, DateTimeOffset>>>()
            { { "data publikacji", n =>n.PublishDateTime }
            };
            SortDictionaryBool = new Dictionary<string, Expression<Func<Comment, bool>>>()
            { { "zaakceptowane", n => n.Allowed }
            };
        }
    }
}
