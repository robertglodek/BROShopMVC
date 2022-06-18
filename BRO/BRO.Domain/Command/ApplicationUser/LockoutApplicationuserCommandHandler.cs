using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LockoutApplicationuserCommandHandler : ICommandHandler<LockoutApplicationuserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LockoutApplicationuserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(LockoutApplicationuserCommand command)
        {
            var validationResult = await new LockoutApplicationuserCommandValidator().ValidateAsync(command);

            if(!validationResult.IsValid)
                return Result.Fail(validationResult);

            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(command.Id);
            if(applicationUser==null)
                return Result.Fail("Użytkownik nie istnieje");
            var result = await _unitOfWork.ApplicationUserRepository.LockoutAsync(applicationUser,command.LockoutEnd);
            if(!result.Succeeded)
                return Result.Fail("Nie udało się zablokowac użytkownika");
            applicationUser.LockoutReason = command.LockoutReason;
            await _unitOfWork.ApplicationUserRepository.UpdateAsync(applicationUser);
            return Result.Ok();
        }
    }
}
