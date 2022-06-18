using AutoMapper;
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
    public class GetApplicationUserQueryHandler:IQueryHandler<GetApplicationUserQuery, ApplicationUserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApplicationUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDTO> HandleAsync(GetApplicationUserQuery query)
        {
            Entities.ApplicationUser applicationUser;
            if (query.Id!=default(Guid))
                applicationUser = await _unitOfWork.ApplicationUserRepository.GetById(query.Id);
            else if(query!=null)
                applicationUser = await _unitOfWork.ApplicationUserRepository.GetByEmail(query.Email);
            else
                applicationUser = null;
            if (applicationUser == null)
                throw new NotFoundException($"Carrier with Id:{query.Id} does not exist.");

            return _mapper.Map<ApplicationUserDTO>(applicationUser);
        }
    }
}
