using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    public class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, PagedResult<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ProductDTO>> HandleAsync(SearchProductsQuery query)
        {
            var validationResult = await new SearchProductsQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var products = await _unitOfWork.ProductRepository.SearchAsync(query,"Reviews,Category,Manufacturer");
            return  new PagedResult<ProductDTO>(_mapper.Map<List<ProductDTO>>(products.PageElements), products.TotalElementsCount, 
                query.PageSize, query.PageNumber, query.SortBy);
        }
    }
}
