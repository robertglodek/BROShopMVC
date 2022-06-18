using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Comment
{
    public sealed class SearchCommentsDetailsQueryHandler : IQueryHandler<SearchCommentsDetailsQuery, PagedResult<CommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchCommentsDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<CommentDTO>> HandleAsync(SearchCommentsDetailsQuery query)
        {
            var validationResult = await new SearchCommentsDetailsQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var comments = await _unitOfWork.CommentRepository.SearchDetailsAsync(query,"Product,User");
            return new PagedResult<CommentDTO>(_mapper.Map<List<CommentDTO>>(comments.PageElements), comments.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
        }

    }
}
