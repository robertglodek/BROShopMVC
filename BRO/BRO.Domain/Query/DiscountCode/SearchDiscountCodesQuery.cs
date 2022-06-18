using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DiscountCode
{
    
    public class SearchDiscountCodesQuery : SearchQuery, IQuery<PagedResult<DiscountCodeDTO>>
    {
     
    }
}
