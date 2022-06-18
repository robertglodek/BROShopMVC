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

    public class EditDiscountCodeCommandHandler : ICommandHandler<EditDiscountCodeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditDiscountCodeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditDiscountCodeCommand command)
        {
            var validationResult = await new EditDiscountCodeCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var discountCode = await _unitOfWork.DiscountCodeRepository.GetById(command.Id);
            if (discountCode == null)
                return Result.Fail("Kod zniżkowy nie istnieje");
            var discountCodeExists = await _unitOfWork.DiscountCodeRepository.CheckIfNameAlreadyExists(command.Id, command.CodeName);
            if (discountCodeExists)
                return Result.Fail("Kod zniżkowy o tej nazwie już istnieje");
            _mapper.Map(command, discountCode);
            await _unitOfWork.DiscountCodeRepository.UpdateAsync(discountCode);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
