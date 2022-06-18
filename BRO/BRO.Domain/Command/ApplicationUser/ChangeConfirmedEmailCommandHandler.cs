using AutoMapper;
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
   
    public class ChangeConfirmedEmailCommandHandler : ICommandHandler<ChangeConfirmedEmailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangeConfirmedEmailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(ChangeConfirmedEmailCommand command)
        {
            var validationResult = await new ChangeConfirmedEmailCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(command.Id);
            if (applicationUser == null)
                return Result.Fail("Nie udało się zmienić adresu email");
            if (command.NewEmail == applicationUser.Email)
                return Result.Fail("Wprowadzono ten sam email");
            if ((await _unitOfWork.ApplicationUserRepository.GetByEmail(command.NewEmail)) != null)
                return Result.Fail("Podany Email jest już zajęty");
            var result= await _unitOfWork.ApplicationUserRepository.ChangeEmailAsync(applicationUser, command.NewEmail, command.Token);
            if (!result.Succeeded)
                return Result.Fail("Nie udało się zmienić adresu email");
            applicationUser.UserName = command.NewEmail;
            applicationUser.NormalizedUserName = command.NewEmail.ToUpper();
            await _unitOfWork.ApplicationUserRepository.UpdateAsync(applicationUser);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
