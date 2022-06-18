using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Review
{
    public class SearchReviewsQuery:SearchQuery,IQuery<PagedResult<ReviewDTO>>
    {
       
    }
}
