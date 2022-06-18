using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository CategoryRepository { get;  }
        ITasteRepository TasteRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        IProductRepository ProductRepository { get; }
        ICarrierRepository CarrierRepository { get; }
        ICommentRepository CommentRepository { get; }
        IProductTasteRepository ProductTasteRepository { get; }
        IShoppingCartItemRepository ShoppingCartItemRepository { get; }
        IOrderHeaderRepository OrderRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; set; }
        IRoleRepository RoleRepository { get; }
        IOrderBillRepository OrderBillRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IDiscountCodeRepository DiscountCodeRepository { get; }
        Task CommitAsync();
    }
}
