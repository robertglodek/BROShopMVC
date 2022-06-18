using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ApplicationUser
{
   
    public class GetPaswordResetTokenQueryHandler : IQueryHandler<GetPaswordResetTokenQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPaswordResetTokenQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> HandleAsync(GetPaswordResetTokenQuery query)
        {
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetByEmail(query.Email);
            if (applicationUser == null)
                throw new NotFoundException($"User with Email: {query.Email} does not exist");
            var token = await _unitOfWork.ApplicationUserRepository.GeneratePasswordResetTokenAsync(applicationUser);
            return token;
        }
    }
}
