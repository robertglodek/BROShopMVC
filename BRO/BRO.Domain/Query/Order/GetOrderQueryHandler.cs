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
   
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderHeaderDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderHeaderDTO> HandleAsync(GetOrderQuery query)
        {
            var order = await _unitOfWork.OrderRepository.GetById(query.Id,"User");
            if (order == null)
                throw new NotFoundException($"Order with Id:{query.Id} does not exist.");
            var orderDTO = _mapper.Map<OrderHeaderDTO>(order);
            return orderDTO;
        }
    }
}
