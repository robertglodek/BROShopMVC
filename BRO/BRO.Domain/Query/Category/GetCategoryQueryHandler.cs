using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Category
{
    public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery,CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> HandleAsync(GetCategoryQuery query)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(query.Id);
            if (category == null)
                throw new NotFoundException($"Category with Id: {query.Id} does not exist");
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
