using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class SignInCommandhandler : ICommandHandler<SignInCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SignInCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(SignInCommand command)
        {
            var user = await _unitOfWork.ApplicationUserRepository.GetByEmail(command.Email);
            if (user == null)
                return Result.Fail("Logowanie nie powiodło się");
             await _unitOfWork.ApplicationUserRepository.SignInAsync(user, command.RememberMe);
            return Result.Ok();
        }
    }
}
