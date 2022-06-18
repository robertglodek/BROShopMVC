using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.IRepositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<int> Count();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> list);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = null, int? count = null);
        
    }
}
