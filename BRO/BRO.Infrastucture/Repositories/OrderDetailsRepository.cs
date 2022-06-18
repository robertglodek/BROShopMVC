using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
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
    public class OrderDetailsRepository:RepositoryAsync<OrderDetails>,IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<OrderDetails>> GetAllByOrderIdAsync(Guid orderId)
        {
            IQueryable<OrderDetails> query = _db.OrderDetails;
            query = query.Where(n=>n.OrderHeaderId==orderId);
            query = query.Include(n => n.ProductTaste).ThenInclude(n => n.Product);
            return await query.ToListAsync();
        }

        public async Task<OrderDetails> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<OrderDetails> query = _db.OrderDetails;
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
