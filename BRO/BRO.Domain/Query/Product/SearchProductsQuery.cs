using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    public class SearchProductsQuery:SearchQuery,IQuery<PagedResult<ProductDTO>>
    {
        public bool OnlyAvailable { get; set; } = false;
        public bool IsDefault()
        {
            if(OnlyAvailable==false && SearchName==null&&SearchValue==null&&PageNumber==1&&
                PageSize==BRO.Domain.Utilities.StaticDetails.PagedResultSizes.GetAllowedSizes().First() && SortBy=="--")
                return true;
            return false;
        }
    }
}
