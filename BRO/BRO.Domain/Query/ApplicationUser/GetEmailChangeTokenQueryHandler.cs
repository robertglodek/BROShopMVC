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
    
    public class GetEmailChangeTokenQueryHandler : IQueryHandler<GetEmailChangeTokenQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmailChangeTokenQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> HandleAsync(GetEmailChangeTokenQuery query)
        {
            var applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(query.Id);
            if (applicationUser == null)
                throw new NotFoundException($"User with Id: {query.Id} does not exist");
            var token = await _unitOfWork.ApplicationUserRepository.GenerateEmailChangeTokenAsync(applicationUser,query.newEmail);
            return token;
        }
    }
}
