using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Role
{
    public class GetRolesQueryHandler:IQueryHandler<GetRolesQuery,List<RoleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<RoleDTO>> HandleAsync(GetRolesQuery query)
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        } 
    }
}
