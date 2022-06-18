using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
  
    public class EditPasswordCommandHandler : ICommandHandler<EditPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditPasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }
        public async Task<Result> HandleAsync(EditPasswordCommand command)
        {
            var validationResult = await new EditPasswordCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(command.Id);
            if (applicationUser == null)
                return Result.Fail("Zmiana hasła nie powiodła się");
            var validCurrentPassword = await _unitOfWork.ApplicationUserRepository.CheckPasswordAsync(applicationUser, command.PasswordToConfirm);
            if (!validCurrentPassword)
                return Result.Fail("Podane obecne hasło jest nieprawidłowe");
            var result = await _unitOfWork.ApplicationUserRepository.ChangePasswordAsync(applicationUser, command.PasswordToConfirm, command.Password);
            if (!result.Succeeded)
                return Result.Fail("Nie udało się zmienić hasła");
            return Result.Ok();
        }

    }
}
