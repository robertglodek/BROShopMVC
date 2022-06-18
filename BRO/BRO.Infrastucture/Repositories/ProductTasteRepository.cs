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
    public class ProductTasteRepository : RepositoryAsync<ProductTaste>, IProductTasteRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductTasteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductTaste>> GetAllByIds(IEnumerable<Guid> ids)
        {
            IQueryable<ProductTaste> baseQuery = _db.ProductTastes;
            baseQuery = baseQuery.Where(n => ids.Contains(n.Id));
            baseQuery = baseQuery.Include(n => n.Taste).Include(n => n.Product);
            return await baseQuery.ToListAsync();
        }

        public async Task<IEnumerable<ProductTaste>> GetAllByProductId(Guid id)
        {
            IQueryable<ProductTaste> baseQuery = _db.ProductTastes;
            baseQuery = baseQuery.Where(n => n.ProductId == id);
            baseQuery = baseQuery.Include(n => n.Taste).Include(n => n.Product);
            return await baseQuery.ToListAsync();
        }
        public async Task<ProductTaste> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<ProductTaste> query = _db.ProductTastes;
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
