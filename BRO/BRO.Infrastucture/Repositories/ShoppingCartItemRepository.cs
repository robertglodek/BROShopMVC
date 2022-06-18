using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.ShoppingCartItem;
using BRO.Infrastructure.Repositories;
using BRO.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Repositories
{
    public class ShoppingCartItemRepository:RepositoryAsync<ShoppingCartItem>,IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ShoppingCartItem> GetByIdWithDetails(Guid id)
        {
            IQueryable<ShoppingCartItem> query = _db.ShoppingCarts;
            query = query.Where(n=>n.Id==id);
            query = query.Include(n => n.ProductTaste).ThenInclude(n => n.Product).Include(n => n.ProductTaste).ThenInclude(n => n.Taste);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShoppingCartItem>> GetAllByUserIdAsync(Guid userId)
        {
            IQueryable<ShoppingCartItem> query = _db.ShoppingCarts;
            query = query.Where(n=>n.ApplicationUserId==userId);
            query =query.Include(n => n.ProductTaste).ThenInclude(n => n.Product).Include(n=>n.ProductTaste).ThenInclude(n=>n.Taste);
            return await query.ToListAsync();
        }
        public int Count(Guid id)
        {
            IQueryable<ShoppingCartItem> query = _db.ShoppingCarts;
            query = query.Where(n=>n.ApplicationUserId==id);
            return query.Count();
        }

        public async  Task<ShoppingCartItem> GetByUserIdAndProductTasteId(Guid userId, Guid productTasteId)
        {
            IQueryable<ShoppingCartItem> baseQuery = _db.ShoppingCarts;
            baseQuery = baseQuery.Where(n => n.ApplicationUserId== userId && n.ProductTasteId== productTasteId);
            return await baseQuery.FirstOrDefaultAsync();
        }
        public async Task<ShoppingCartItem> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<ShoppingCartItem> query = _db.ShoppingCarts;
            query = query.Where(n => n.Id == Id);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
