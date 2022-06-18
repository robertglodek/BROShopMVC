using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BRO.Domain.Query.Carrier
{
    public sealed class SearchCarriersQueryHandler:IQueryHandler<SearchCarriersQuery, PagedResult<CarrierDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchCarriersQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<CarrierDTO>> HandleAsync(SearchCarriersQuery query)
        {
            var validationResult = await new SearchCarriersQueryValidator().ValidateAsync(query);
            if(validationResult.IsValid==false)
                throw new ValidationException(validationResult.Errors);
            var carriers =  await _unitOfWork.CarrierRepository.SearchAsync(query);
            return new PagedResult<CarrierDTO>(_mapper.Map<List<CarrierDTO>>(carriers.PageElements),carriers.TotalElementsCount,query.PageSize,query.PageNumber, query.SortBy);  
        }
    }
}
