using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IReviewRepository:IRepositoryAsync<Review>
    {
        Task<PagedResult<Review>> SearchAsync(SearchReviewsQuery query, string propertiesToInclude = null);
        Task<Review> GetByProductIdAndUserId(Guid userId, Guid productId,string includeProperties=null);
    }
}
