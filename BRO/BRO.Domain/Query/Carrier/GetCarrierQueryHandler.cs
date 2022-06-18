using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Carrier
{
    public class GetCarrierQueryHandler : IQueryHandler<GetCarrierQuery, CarrierDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCarrierQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CarrierDTO> HandleAsync(GetCarrierQuery query)
        {
            var carrier= await _unitOfWork.CarrierRepository.GetById(query.Id);
            if (carrier == null)
                throw new NotFoundException($"Carrier with Id: {query.Id} does not exist");
            return _mapper.Map<CarrierDTO>(carrier);
        }
    }
}
