
using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Carrier
{
    public class EditCarrierCommandHandler:ICommandHandler<EditCarrierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditCarrierCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditCarrierCommand command)
        {
            var validationResult = await new EditCarrierCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var carrier = await _unitOfWork.CarrierRepository.GetById(command.Id);
            if (carrier== null)
                return Result.Fail("Dostawca nie istnieje");
            var carrierExists = await _unitOfWork.CarrierRepository.CheckIfNameAlreadyExists(command.Id,command.Name);
            if (carrierExists)
                return Result.Fail("Dostawca o tej nazwie już istnieje");
            if (command.Image == null && carrier.Image!=null)
                command.Image = carrier.Image;
            _mapper.Map(command, carrier);
            await _unitOfWork.CarrierRepository.UpdateAsync(carrier);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
