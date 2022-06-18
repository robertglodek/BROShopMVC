using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IOrderDetailsRepository:IRepositoryAsync<OrderDetails>
    {
        Task<IEnumerable<OrderDetails>> GetAllByOrderIdAsync(Guid orderId);
        Task<OrderDetails> GetById(Guid Id, string includeProperties = null);
    }
}
