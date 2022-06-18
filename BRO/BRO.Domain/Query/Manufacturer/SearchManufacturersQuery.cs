using BRO.Domain.Query.DTO.Manufacturer;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Query.Manufacturer
{
    public class SearchManufacturersQuery:SearchQuery,IQuery<PagedResult<ManufacturerDTO>>
    {  
    }
}
