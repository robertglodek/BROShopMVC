using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSortingRules
{
    public class ProductSortingRules :SortingRules<Product>
    {
        public Dictionary<string, Expression<Func<Product, double>>> SortDictionaryNumber { get; private set; }
        public ProductSortingRules()
           : base(new List<string>() { "nazwa", "cena" }, new Dictionary<string, Expression<Func<Product, string>>>()
            {{ "nazwa", n => n.Name }})
        {
            SortDictionaryNumber = new Dictionary<string, Expression<Func<Product, double>>>()
            { { "cena", n => n.IsDiscount==true?n.PromotionalPrice:n.RegularPrice }
            };
        }
    }
}
