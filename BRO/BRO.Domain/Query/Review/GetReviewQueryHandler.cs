using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Review;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Review
{
    public class GetReviewQueryHandler : IQueryHandler<GetReviewQuery, ReviewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReviewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReviewDTO> HandleAsync(GetReviewQuery query)
        {
            var review = await _unitOfWork.ReviewRepository.GetByProductIdAndUserId(query.UserId,query.ProductId,"Product,User");
            return _mapper.Map<ReviewDTO>(review);
        }
    }
}
