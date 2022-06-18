using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Manufacturer
{
    public class EditManufacturerCommandHandler : ICommandHandler<EditManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditManufacturerCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditManufacturerCommand command)
        {
            var validationResult = await new EditManufacturerCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var manufacturer = await _unitOfWork.ManufacturerRepository.GetById(command.Id);
            if (manufacturer == null)
                return Result.Fail("Producent nie istnieje");
            var manufacturerExists = await _unitOfWork.ManufacturerRepository.CheckIfNameAlreadyExists(command.Id, command.Name);
            if (manufacturerExists)
                return Result.Fail("Producent o tej nazwie już istnieje");
            _mapper.Map(command,manufacturer);
            await _unitOfWork.ManufacturerRepository.UpdateAsync(manufacturer);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
