using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Query.Carrier
{
    public class SearchCarriersQuery:SearchQuery,IQuery<PagedResult<CarrierDTO>>
    {
    }
}
