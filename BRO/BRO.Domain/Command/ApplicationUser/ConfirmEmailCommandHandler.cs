using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ConfirmEmailCommandHandler:ICommandHandler<ConfirmEmailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConfirmEmailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(ConfirmEmailCommand command)
        {
            var validationResult = await new ConfirmEmailCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email);
            if (applicationUser == null)
                return Result.Fail("Potwierdzanie adresu email nie powiodło się");
            var result = await _unitOfWork.ApplicationUserRepository.ConfirmEmailAsync(applicationUser, command.token);
            if(!result.Succeeded)
                return Result.Fail("Potwierdzanie adresu email nie powiodło się");
            return Result.Ok();
        }
    }
}
