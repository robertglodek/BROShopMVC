using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
    public class GetShoppingCartItemsQueryHandler:IQueryHandler<GetShoppingCartItemsQuery, List<ShoppingCartItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetShoppingCartItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ShoppingCartItemDTO>> HandleAsync(GetShoppingCartItemsQuery query)
        {
            var shoppingCartItems= await _unitOfWork.ShoppingCartItemRepository.GetAllByUserIdAsync(query.ApplicationUserId);
            return _mapper.Map<List<ShoppingCartItemDTO>>(shoppingCartItems);
        } 
    }
}
