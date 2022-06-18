using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.StaticDetails;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LoginApplicationUserCommandHandler:ICommandHandler<LoginApplicationUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
   
        public LoginApplicationUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<Result> HandleAsync(LoginApplicationUserCommand command)
        {
            var validationResult = await new LoginApplicationUserCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var user = await _unitOfWork.ApplicationUserRepository.GetByEmailWithDetails(command.Email);
            if(user==null)
                return Result.Fail("Logowanie nie powiodło się");
            if(user.UserRoles.FirstOrDefault().Role.Name== BRO.Domain.Utilities.StaticDetails.Roles.RoleAdmin 
                || user.UserRoles.FirstOrDefault().Role.Name == BRO.Domain.Utilities.StaticDetails.Roles.RoleEmployee)
            {
                if (user.EmailConfirmed == false)
                    return Result.Fail(IdentityDetails.LoginEmailNotConfirmed);
            }
            var result = await _unitOfWork.ApplicationUserRepository.PasswordSignInAsync(command.Email, command.Password, command.RememberMe, false);
            if (result.IsLockedOut == true)
                return Result.Fail($"Konto zablokowane do {user.LockoutEnd.Value.LocalDateTime.ToString("MM/dd/yyyy HH:mm")}");
            if (!result.Succeeded)
                return Result.Fail("Logowanie nie powiodło się");
            return Result.Ok();
        }
    }
}
