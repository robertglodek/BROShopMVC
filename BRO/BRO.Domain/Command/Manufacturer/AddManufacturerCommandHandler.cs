using AutoMapper;
using BRO.Domain.Command;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Manufacturer
{
    public class AddManufacturerCommandHandler : ICommandHandler<AddManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddManufacturerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddManufacturerCommand command)
        {
            var validationResult = await new AddManufacturerCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var manufacturerExists = await _unitOfWork.ManufacturerRepository.GetByName(command.Name) is null;
            if (!manufacturerExists)
                return Result.Fail("Producent o tej nazwie już istnieje");
            var manufacturer = _mapper.Map<Entities.Manufacturer>(command);
            await _unitOfWork.ManufacturerRepository.AddAsync(manufacturer);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
