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
    public class ForgotPasswordCommandHandler:ICommandHandler<ForgotPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(ForgotPasswordCommand command)
        {
            var validationResult = await new ForgotPasswordCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);

            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email);
            if (applicationUser == null)
                return Result.Fail();

            return Result.Ok();

        }
    }
}
