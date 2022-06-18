using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace BRO.Domain.Query.Category
{
    public sealed class SearchCategoriesQueryHandler:IQueryHandler<SearchCategoriesQuery,PagedResult<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchCategoriesQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<CategoryDTO>> HandleAsync(SearchCategoriesQuery query)
        {
            var validationResult = await new SearchCategoriesQueryValidator().ValidateAsync(query);
            if(validationResult.IsValid==false)
                throw new ValidationException(validationResult.Errors);
            var categories =  await _unitOfWork.CategoryRepository.SearchAsync(query);
            return new PagedResult<CategoryDTO>(_mapper.Map<List<CategoryDTO>>(categories.PageElements),categories.TotalElementsCount,query.PageSize,query.PageNumber, query.SortBy);  
        }
    }
}
