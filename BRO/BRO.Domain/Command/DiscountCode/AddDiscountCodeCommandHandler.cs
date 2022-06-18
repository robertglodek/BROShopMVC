using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.DiscountCode
{
    public class AddDiscountCodeCommandHandler : ICommandHandler<AddDiscountCodeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddDiscountCodeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddDiscountCodeCommand command)
        {
            var validationResult = await new AddDiscountCodeCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var discountCodeExists = await _unitOfWork.DiscountCodeRepository.GetByName(command.CodeName.ToLower()) is  null;
            if (!discountCodeExists)
                return Result.Fail("Kod zniżkowy o tej nazwie już istnieje");
            var discountCode = _mapper.Map<Entities.DiscountCode>(command);
            await _unitOfWork.DiscountCodeRepository.AddAsync(discountCode);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
