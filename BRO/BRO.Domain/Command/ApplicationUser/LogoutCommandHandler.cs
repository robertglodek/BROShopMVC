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
    public class LogoutCommandHandler:ICommandHandler<LogoutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogoutCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(LogoutCommand command)
        {
            await _unitOfWork.ApplicationUserRepository.SignOutAsync();    
            return Result.Ok();

        }
    }
}
