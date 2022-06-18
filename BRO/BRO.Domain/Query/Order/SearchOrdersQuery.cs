
using BRO.Domain.Query.DTO.OrderHeader;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
    
    public class SearchOrdersQuery :SearchQuery, IQuery<PagedResult<OrderHeaderDTO>>
    {
 
    }
}
