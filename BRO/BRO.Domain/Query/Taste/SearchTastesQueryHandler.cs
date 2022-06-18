using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Taste;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Taste
{
    public class SearchTastesQueryHandler : IQueryHandler<SearchTastesQuery, PagedResult<TasteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchTastesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<TasteDTO>> HandleAsync(SearchTastesQuery query)
        {
            var validationResult = await new SearchTastesQueryValidator().ValidateAsync(query);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.Errors);
            var tastes = await _unitOfWork.TasteRepository.SearchAsync(query);
            return new PagedResult<TasteDTO>(_mapper.Map<List<TasteDTO>>(tastes.PageElements), tastes.TotalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
        }
    }
}
