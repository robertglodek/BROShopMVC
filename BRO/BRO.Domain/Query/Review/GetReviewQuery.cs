using BRO.Domain.Query.DTO.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Review
{
    public class GetReviewQuery:IQuery<ReviewDTO>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
