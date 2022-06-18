using AutoMapper;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.StaticDetails;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.ApplicationUser
{
    public class AddApplicationUserCommandHandler:ICommandHandler<AddApplicationUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddApplicationUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;      
        }
        public async Task<Result> HandleAsync(AddApplicationUserCommand command)
        {
            var validationResult = await new AddApplicationUserCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var userExists = await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email) is null;
            if (!userExists)
                return Result.Fail("Podany Email jest już zajęty");
            var user = new BRO.Domain.Entities.ApplicationUser() { Email=command.Email,UserName=command.Email,PhoneNumber=command.PhoneNumber,
                FirstName=command.FirstName,LastName=command.LastName,CreateDate=DateTimeOffset.Now };
            var rolesCount = await _unitOfWork.RoleRepository.Count();
            var result = await _unitOfWork.ApplicationUserRepository.CreateAsync(user, command.Password);
            if (!result.Succeeded)
                return Result.Fail("Nie udało się założyć konta, spróbuj ponownie później");   
            if(command.Role==null)              
                await _unitOfWork.ApplicationUserRepository.AddToRoleAsync(user, BRO.Domain.Utilities.StaticDetails.Roles.RoleUserIndividual);
            else   
                await _unitOfWork.ApplicationUserRepository.AddToRoleAsync(user, command.Role);
            return Result.Ok();
        }
    }
}
