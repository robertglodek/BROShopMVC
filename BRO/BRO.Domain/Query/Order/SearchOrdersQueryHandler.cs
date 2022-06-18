using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.OrderHeader;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
   
    public class SearchOrdersQueryHandler : IQueryHandler<SearchOrdersQuery, PagedResult<OrderHeaderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<OrderHeaderDTO>> HandleAsync(SearchOrdersQuery query)
        {
            var validationResult = await new SearchOrdersQueryValidator().ValidateAsync(query);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var orders = await _unitOfWork.OrderRepository.SearchAsync(query);
            return new PagedResult<OrderHeaderDTO>(_mapper.Map<List<OrderHeaderDTO>>(orders.PageElements), orders.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
        }
    }
}
