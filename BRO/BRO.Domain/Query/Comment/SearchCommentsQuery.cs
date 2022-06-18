using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Comment
{
    public class SearchCommentsQuery:IQuery<CommentsPagedResult<CommentDTO>>
    {
        public Guid ProductId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
