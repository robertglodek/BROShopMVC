using BRO.Domain.Entities;
using BRO.Domain.Query.Comment;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface  ICommentRepository:IRepositoryAsync<Comment>
    {
        Task UpdateAsync(Comment comment);
        Task<CommentsPagedResult<Comment>> SearchAsync(SearchCommentsQuery query, string propertiesToInclude = null);
        Task<PagedResult<Comment>> SearchDetailsAsync(SearchCommentsDetailsQuery query, string propertiesToInclude = null);
        Task<Comment> GetById(Guid Id, string includeProperties = null);
    }
}
