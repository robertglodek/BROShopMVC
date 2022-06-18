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
    public class EditTasteCommandHandler:ICommandHandler<EditTasteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditTasteCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditTasteCommand command)
        {
            var validationResult = await new EditTasteCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var taste = await _unitOfWork.TasteRepository.GetById(command.Id);
            if (taste == null)
                return Result.Fail("Smak nie istnieje.");
            var tasteExists = await _unitOfWork.TasteRepository.CheckIfNameAlreadyExists(command.Id, command.Name);
            if (tasteExists)
                return Result.Fail("Smak o tej nazwie już istnieje");
            _mapper.Map(command,taste);
            await _unitOfWork.TasteRepository.UpdateAsync(taste);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

    }
}
