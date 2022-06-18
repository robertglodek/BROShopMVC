using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteCommentCommand command)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(command.Id);
            if (comment == null)
                return Result.Fail("Komentarz nie istnieje");
            await _unitOfWork.CommentRepository.RemoveAsync(comment);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
