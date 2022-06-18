using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IOrderHeaderRepository:IRepositoryAsync<OrderHeader>
    {
        Task UpdateAsync(OrderHeader category);
        Task<PagedResult<OrderHeader>> SearchAsync(BRO.Domain.Query.Order.SearchOrdersQuery query, string propertiesToInclude = null);
        Task<OrderHeader> GetByIdWithDetails(Guid id);
        Task<IEnumerable<OrderHeader>> GetAllForUser(Guid userId);
        Task<OrderHeader> GetById(Guid Id, string includeProperties = null);
    }
}
