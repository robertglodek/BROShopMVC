using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IShoppingCartItemRepository:IRepositoryAsync<ShoppingCartItem>
    {
        int Count(Guid userId);
        Task<IEnumerable<ShoppingCartItem>> GetAllByUserIdAsync(Guid userId);
        Task<ShoppingCartItem> GetByIdWithDetails(Guid id);
        Task<ShoppingCartItem> GetByUserIdAndProductTasteId(Guid userId, Guid productTasteId);
        Task<ShoppingCartItem> GetById(Guid Id, string includeProperties = null);


    }
}
