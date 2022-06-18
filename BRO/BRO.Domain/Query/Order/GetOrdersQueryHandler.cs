using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
    public class GetOrdersQueryHandler:IQueryHandler<GetOrdersQuery,List<OrderHeaderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrderHeaderDTO>> HandleAsync(GetOrdersQuery query)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllForUser(query.UserId);
            return _mapper.Map<List<OrderHeaderDTO>>(orders.OrderByDescending(s => s.OrderDate));
        }
    }
}
