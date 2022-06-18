using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.OrderDetails;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.OrderDetails
{
   
    public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, List<OrderDetailsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailsDTO>> HandleAsync(GetOrderDetailsQuery query)
        {
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetAllByOrderIdAsync(query.OrderId);
            return _mapper.Map<List<OrderDetailsDTO>>(orderDetails);
        }
    }
}
