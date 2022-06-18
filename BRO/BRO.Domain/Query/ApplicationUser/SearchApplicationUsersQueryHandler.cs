using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ApplicationUser
{
    public class SearchApplicationUsersQueryHandler:IQueryHandler<SearchApplicationUsersQuery,PagedResult<ApplicationUserDTO>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchApplicationUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ApplicationUserDTO>> HandleAsync(SearchApplicationUsersQuery query)
        {
            var validationResult = await new SearchApplicationUsersQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var users = await _unitOfWork.ApplicationUserRepository.SearchAsync(query,n=>n.UserRoles,s=>s.Role);
            var result = new PagedResult<ApplicationUserDTO>(_mapper.Map<List<ApplicationUserDTO>>(users.PageElements), users.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            for (int i = 0; i < users.PageElements.Count(); i++)
                result.PageElements[i].RoleName = users.PageElements[i].UserRoles.FirstOrDefault().Role.Name;
            return result;
        }
    }
}
