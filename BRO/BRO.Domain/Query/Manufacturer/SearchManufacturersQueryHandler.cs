using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Manufacturer;
using BRO.Domain.Query.DTO.Pagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Manufacturer
{
    public class SearchManufacturersQueryHandler : IQueryHandler<SearchManufacturersQuery, PagedResult<ManufacturerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchManufacturersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResult<ManufacturerDTO>> HandleAsync(SearchManufacturersQuery query)
        {

            var validationResult = await new SearchManufacturersQueryValidator().ValidateAsync(query);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var categories = await _unitOfWork.ManufacturerRepository.SearchAsync(query);


            return new PagedResult<ManufacturerDTO>(_mapper.Map<List<ManufacturerDTO>>(categories.PageElements), categories.TotalElementsCount, query.PageSize, query.PageNumber,query.SortBy);



        }
    }
}
