using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Taste
{
    public class AddTasteCommandHandler : ICommandHandler<AddTasteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddTasteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddTasteCommand command)
        {
            var validationResult = await new AddTasteCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var tasteExists = await _unitOfWork.TasteRepository.GetByName(command.Name) is  null;
            if (!tasteExists)
                return Result.Fail("Smak o tej nazwie już istnieje");
            var taste = _mapper.Map<Entities.Taste>(command);
            await _unitOfWork.TasteRepository.AddAsync(taste);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
