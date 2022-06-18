using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Manufacturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Manufacturer
{
    public class GetManufacturersQueryHandler : IQueryHandler<GetManufacturersQuery, List<ManufacturerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetManufacturersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ManufacturerDTO>> HandleAsync(GetManufacturersQuery query)
        {
            var manufacturers = await _unitOfWork.ManufacturerRepository.GetAllAsync();
            return _mapper.Map<List<ManufacturerDTO>>(manufacturers);
        }
    }
}
