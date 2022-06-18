using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ApplicationUser
{
    public class SearchApplicationUsersQuery:SearchQuery,IQuery<PagedResult<ApplicationUserDTO>>
    {     
    }
}
