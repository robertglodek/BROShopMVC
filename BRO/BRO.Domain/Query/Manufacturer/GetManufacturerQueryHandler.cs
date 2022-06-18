using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Manufacturer;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Manufacturer
{
    public class GetManufacturerQueryHandler:IQueryHandler<GetManufacturerQuery,ManufacturerDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetManufacturerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ManufacturerDTO> HandleAsync(GetManufacturerQuery query)
        {
            var manufacturer = await _unitOfWork.ManufacturerRepository.GetById(query.Id);  
            if (manufacturer == null)
                throw new NotFoundException($"Manufacturer with Id: {query.Id} does not exist");
            return _mapper.Map<ManufacturerDTO>(manufacturer);
        }
    }
}
