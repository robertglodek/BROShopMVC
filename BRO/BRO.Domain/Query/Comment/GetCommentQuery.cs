using BRO.Domain.Query.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Comment
{
    public class GetCommentQuery:IQuery<CommentDTO>
    {
        public Guid Id { get; set; }
    }
}
