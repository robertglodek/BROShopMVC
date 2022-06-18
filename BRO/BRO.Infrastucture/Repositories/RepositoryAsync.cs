using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities.PaginationSearchByRules;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastructure.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public RepositoryAsync(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            
            await dbSet.AddAsync(entity);
            string pawel = "dd";
        }
        public async Task<IEnumerable<T>> GetAllAsync(string propertiesToInclude = null,int? count=null)
        {
            IQueryable<T> query = dbSet;
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if(count!=null)
                query.Take((int)count);
            return await query.ToListAsync();
        }
        public async Task<int> Count()
        {
            return await dbSet.CountAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            T obj =  await dbSet.FindAsync(id);
            await RemoveAsync(obj);
        }
        public async Task RemoveAsync(T obj)
        {
            dbSet.Remove(obj);
        }
        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
        public async Task AddRangeAsync(IEnumerable<T> list)
        {
            await dbSet.AddRangeAsync(list);
        }
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }
        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            dbSet.UpdateRange(entities);
        } 
    }
}
