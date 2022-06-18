using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.PaginationSearchByRules
{
    public class ProductSearchByRules : SearchByRules<Product>
    {
        public ProductSearchByRules()
        {
            SearchByList = new List<string>() { "name","category","manufacturer","discount","latest","tag" };
        }
        public ProductSearchByRules(string searchValue)
        {
            SearchByList = new List<string>() { "name", "category", "manufacturer", "discount", "latest", "tag" };
            SearchByDictionary = new Dictionary<string, Expression<Func<Product, bool>>>()
            {
                    {"name",n=>searchValue!=""&&n.Name.ToLower().Contains(searchValue.ToLower())},
                    {"category",n=>searchValue!=""&&n.Category.Name.ToLower()==searchValue.ToLower()},
                    {"manufacturer",n=>searchValue!=""&&n.Manufacturer.Name.ToLower()==searchValue.ToLower()},
                    {"tag",n=>searchValue!=""&&n.SearchTag.ToLower()==searchValue.ToLower()},
                    {"discount",n=>n.IsDiscount==true},
                    {"latest",n=>n.ProductAddDate.AddDays(BRO.Domain.Utilities.StaticDetails.Other.DisplayAsNewProductsInDays).CompareTo(DateTimeOffset.Now)>=0},
            };
        }
    }
}
