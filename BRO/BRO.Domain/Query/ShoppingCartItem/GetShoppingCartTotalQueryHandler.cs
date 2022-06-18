using BRO.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
    public class GetShoppingCartTotalQueryHandler:IQueryHandler<GetShoppingCartTotalQuery,double>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShoppingCartTotalQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<double> HandleAsync(GetShoppingCartTotalQuery query)
        {
            var shoppingCartItems = await _unitOfWork.ShoppingCartItemRepository.GetAllByUserIdAsync(query.UserId);
            double orderTotal = 0;
            foreach (var item in shoppingCartItems)
            {
                var price= item.ProductTaste.Product.IsDiscount == true ? item.ProductTaste.Product.PromotionalPrice : item.ProductTaste.Product.RegularPrice;
                orderTotal += price * item.Count; 
            }
            return orderTotal;
        }

    }
}
