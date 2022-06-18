using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Comment;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Comment
{
   
    public class GetCommentQueryHandler : IQueryHandler<GetCommentQuery, CommentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCommentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentDTO> HandleAsync(GetCommentQuery query)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(query.Id,"User,Product");
            if (comment == null)
                throw new NotFoundException($"Comment with Id: {query.Id} does not exist");
            return _mapper.Map<CommentDTO>(comment);


        }
    }
}
