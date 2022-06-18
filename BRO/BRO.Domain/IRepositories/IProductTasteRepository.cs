using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IProductTasteRepository : IRepositoryAsync<ProductTaste>
    {
        Task<IEnumerable<ProductTaste>> GetAllByProductId(Guid id);
        Task<IEnumerable<ProductTaste>> GetAllByIds(IEnumerable<Guid> ids);
        Task<ProductTaste> GetById(Guid Id, string includeProperties = null);
    }
}
