using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderHeader
{
    public class EditOrderCommandHandler : ICommandHandler<EditOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditOrderCommand command)
        {
            var validationResult = await new EditOrderCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var order =await _unitOfWork.OrderRepository.GetById(command.Id);
            if (order==null)
                return Result.Fail("Nie udało się zmodyfikować zamówienia");
            if (order.ConcurrencyStamp != command.ConcurrencyStamp)
                return Result.Fail("Praca na nieaktualnych danych");
            _mapper.Map(command, order);
            order.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
