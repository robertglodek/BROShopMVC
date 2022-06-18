using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Review;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Review
{
    public sealed class SearchReviewsQueryHandler : IQueryHandler<SearchReviewsQuery, PagedResult<ReviewDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReviewDTO>> HandleAsync(SearchReviewsQuery query)
        {
            var validationResult = await new SearchReviewsQueryValidator().ValidateAsync(query);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var reviews = await _unitOfWork.ReviewRepository.SearchAsync(query,"Product,User");
            return new PagedResult<ReviewDTO>(_mapper.Map<List<ReviewDTO>>(reviews.PageElements), reviews.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
        }
    }
}
