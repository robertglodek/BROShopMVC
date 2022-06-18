
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
    public class AddCarrierCommandHandler:ICommandHandler<AddCarrierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCarrierCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddCarrierCommand command)
        {
            var validationResult = await new AddCarrierCommandValidator().ValidateAsync(command);
            if(validationResult.IsValid==false)
                return Result.Fail(validationResult);
            var carrierExists = await _unitOfWork.CarrierRepository.GetByName(command.Name) is null;
            if(!carrierExists)
                return Result.Fail("Dostawca o tej nazwie już istnieje");
            var carrier = _mapper.Map<Entities.Carrier>(command);
            await _unitOfWork.CarrierRepository.AddAsync(carrier);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

       
    }
}
