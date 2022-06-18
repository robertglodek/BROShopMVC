using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Comment
{
   
    public class SearchCommentsDetailsQuery :SearchQuery ,IQuery<PagedResult<CommentDTO>>
    {
    }
}
