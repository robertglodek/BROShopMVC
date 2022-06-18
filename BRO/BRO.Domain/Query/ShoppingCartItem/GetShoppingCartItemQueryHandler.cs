using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
   

    public class GetShoppingCartItemQueryHandler : IQueryHandler<GetShoppingCartItemQuery, ShoppingCartItemDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ShoppingCartItemDTO> HandleAsync(GetShoppingCartItemQuery query)
        {
            var shoppingCartItem = await _unitOfWork.ShoppingCartItemRepository.GetByIdWithDetails(query.Id);
            if (shoppingCartItem == null)
                throw new NotFoundException($"ShoppingCartItem with Id: {query.Id} does not exist");
            return _mapper.Map<ShoppingCartItemDTO>(shoppingCartItem);
        }
    }
}
