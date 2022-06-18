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
    public class AddCommentCommandHandler:ICommandHandler<AddCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddCommentCommand command)
        {
            var validationResult = await new AddCommentCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            if ((await _unitOfWork.ApplicationUserRepository.GetById(command.UserId)==null) ||
                (await _unitOfWork.ProductRepository.GetById(command.ProductId)==null))
                return Result.Fail("Nie udało się dodać komentarza");
            var comment = _mapper.Map<Entities.Comment>(command);
            comment.PublishDateTime = DateTimeOffset.Now;
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
