using BRO.Domain.Entities;
using BRO.Domain.Query.Carrier;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface ICarrierRepository:IRepositoryAsync<Carrier>
    {
        Task UpdateAsync(Carrier category);
        Task<PagedResult<Carrier>> SearchAsync(SearchCarriersQuery query, string propertiesToInclude = null);
        Task<Carrier> GetByName(string name, string includeProperties = null);
        Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
        Task<Carrier> GetById(Guid Id, string includeProperties = null);
    }
}
