using AutoMapper;
using BRO.Domain.Command.Review;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Comment
{
    public class AddReviewCommandHandler : ICommandHandler<AddReviewCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddReviewCommand command)
        {
            var validationResult = await new AddReviewCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var review = _mapper.Map<Entities.Review>(command);
            review.PublishDateTime = DateTimeOffset.Now;
            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}