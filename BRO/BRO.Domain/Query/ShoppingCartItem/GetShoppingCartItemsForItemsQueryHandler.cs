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
   
    public class GetShoppingCartItemsForItemsQueryHandler : IQueryHandler<GetShoppingCartItemsForItemsQuery, List<ShoppingCartItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartItemsForItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ShoppingCartItemDTO>> HandleAsync(GetShoppingCartItemsForItemsQuery query)
        {
            var productTastes = await _unitOfWork.ProductTasteRepository.GetAllByIds( query.ProductTasteIds.Select(n => n.ProductTasteId));
            var shoppingCartItems = new List<Entities.ShoppingCartItem>();
            foreach (var item in productTastes)
            {
                var newItem = new Entities.ShoppingCartItem() { ProductTaste = item, ProductTasteId = item.Id };
                newItem.Count = query.ProductTasteIds.FirstOrDefault(n => n.ProductTasteId == item.Id).Count;
                newItem.Id = query.ProductTasteIds.FirstOrDefault(n => n.ProductTasteId == item.Id).Id;
                shoppingCartItems.Add(newItem);
            }
            return _mapper.Map<List<ShoppingCartItemDTO>>(shoppingCartItems);
        }
    }
}
