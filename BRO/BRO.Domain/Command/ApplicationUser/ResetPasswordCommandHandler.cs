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
    class ResetPasswordCommandHandler:ICommandHandler<ResetPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Entities.ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, UserManager<BRO.Domain.Entities.ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result> HandleAsync(ResetPasswordCommand command)
        {
            var validationResult = await new ResetPasswordCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            if(command.Email==null || command.Token == null)
                return Result.Fail("Resetowanie hasła nie powiodło się");
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email);
            if (applicationUser == null)
                return Result.Fail("Użytkownik nie istnieje");
            var result = await _unitOfWork.ApplicationUserRepository.ResetPassowrdAsync(applicationUser, command.Token, command.Password);
            if(!result.Succeeded)
                return Result.Fail("Resetowanie hasła nie powiodło się");
            return Result.Ok();

        }
    }
}
