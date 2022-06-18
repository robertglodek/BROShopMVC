using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{

    public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditCommentCommand command)
        {  
            var comment = await _unitOfWork.CommentRepository.GetById(command.Id);
            if (command == null)
                return Result.Fail("Komentarz nie istnieje");
            _mapper.Map(command, comment);
            await _unitOfWork.CommentRepository.UpdateAsync(comment);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
