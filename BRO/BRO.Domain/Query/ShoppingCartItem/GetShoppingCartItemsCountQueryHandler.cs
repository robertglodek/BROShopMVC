using BRO.Domain.IRepositories;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
    public class GetShoppingCartItemsCountQueryHandler:IQueryHandler<GetShoppingCartItemsCountQuery,int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShoppingCartItemsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> HandleAsync(GetShoppingCartItemsCountQuery query)
        {
            if(query.UserId !=default(Guid))
                return _unitOfWork.ShoppingCartItemRepository.Count(query.UserId);
            return default(int);
        }
    }
}
