
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Taste;
using BRO.Domain.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Query.Taste
{
    public class SearchTastesQuery :SearchQuery, IQuery<PagedResult<TasteDTO>>
    {   
    }
}
