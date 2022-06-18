using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DiscountCode
{
    public sealed class SearchDiscountCodesQueryHandler : IQueryHandler<SearchDiscountCodesQuery, PagedResult<DiscountCodeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchDiscountCodesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<DiscountCodeDTO>> HandleAsync(SearchDiscountCodesQuery query)
        {
            var validationResult = await new SearchDiscountCodesQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var discountCodes = await _unitOfWork.DiscountCodeRepository.SearchAsync(query);
            return new PagedResult<DiscountCodeDTO>(_mapper.Map<List<DiscountCodeDTO>>(discountCodes.PageElements), discountCodes.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
        }
    }
}
