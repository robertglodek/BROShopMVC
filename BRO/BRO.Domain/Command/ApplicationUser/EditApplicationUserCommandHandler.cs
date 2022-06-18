using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
   
    public class EditApplicationUserCommandHandler : ICommandHandler<EditApplicationUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditApplicationUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditApplicationUserCommand command)
        {
            var validationResult = await new EditApplicationUserCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(command.Id);
            if (applicationUser == null)
                return Result.Fail("Zmiana danych nie powiodła się");
            _mapper.Map(command, applicationUser);
            await _unitOfWork.ApplicationUserRepository.UpdateAsync(applicationUser);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

    }
}
