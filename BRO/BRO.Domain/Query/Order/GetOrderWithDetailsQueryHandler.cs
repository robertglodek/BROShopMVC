using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.OrderHeader;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
    public class GetOrderWithDetailsQueryHandler : IQueryHandler<GetOrderWithDetailsQuery, OrderHeaderDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderWithDetailsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<OrderHeaderDetailsDTO> HandleAsync(GetOrderWithDetailsQuery query)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetails(query.Id);
            if (order == null)
                throw new NotFoundException($"Order with Id:{query.Id} does not exist");
            var orderDTO = _mapper.Map<OrderHeaderDetailsDTO>(order);
            return orderDTO;
        }
    }
}
