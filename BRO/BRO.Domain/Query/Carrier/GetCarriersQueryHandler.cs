using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Carrier
{
    public class GetCarriersQueryHandler : IQueryHandler<GetCarriersQuery, List<CarrierDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCarriersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CarrierDTO>> HandleAsync(GetCarriersQuery query)
        {
            var carriers = await _unitOfWork.CarrierRepository.GetAllAsync();
            return _mapper.Map<List<CarrierDTO>>(carriers);  
        }
    }
}
