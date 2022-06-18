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
    public class ChangeEmailCommandhandler:ICommandHandler<ChangeEmailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangeEmailCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<Result> HandleAsync(ChangeEmailCommand command)
        {
            var validationResult = await new ChangeEmailCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(command.Id);
            if (applicationUser == null)
                return Result.Fail("Nie udało się zmienić adresu email");
            var validPassword = await _unitOfWork.ApplicationUserRepository.CheckPasswordAsync(applicationUser, command.PasswordToConfirm);
            if (!validPassword)
                return Result.Fail("Podano nieprawidłowe hasło");
            if (command.Email == applicationUser.Email)
                return Result.Fail("Wprowadzono ten sam email");
            if ((await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email))!=null)
                return Result.Fail("Podany Email jest już zajęty");

            if (applicationUser.EmailConfirmed)
                return Result.Fail(IdentityDetails.EmailChange);

            applicationUser.Email = command.Email;
            applicationUser.NormalizedEmail = command.Email.ToUpper();
            applicationUser.UserName = command.Email;
            applicationUser.NormalizedUserName = command.Email.ToUpper();
            await _unitOfWork.ApplicationUserRepository.UpdateAsync(applicationUser);
            await _unitOfWork.CommitAsync();
            return Result.Ok();


        }
    }
}
