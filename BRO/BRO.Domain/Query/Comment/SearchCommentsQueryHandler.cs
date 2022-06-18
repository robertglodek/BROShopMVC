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
    public class SearchCommentsQueryHandler:IQueryHandler<SearchCommentsQuery, CommentsPagedResult<CommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentsPagedResult<CommentDTO>> HandleAsync(SearchCommentsQuery query)
        {
            var validationResult = await new SearchCommentsQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var comments = await _unitOfWork.CommentRepository.SearchAsync(query,"User");
            return new CommentsPagedResult<CommentDTO>(_mapper.Map<List<CommentDTO>>(comments.Comments), comments.TotalElementsCount, query.PageSize, query.PageNumber);
        }
    }
}
