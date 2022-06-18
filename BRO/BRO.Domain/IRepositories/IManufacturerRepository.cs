using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.Manufacturer;

namespace BRO.Domain.IRepositories
{
    public interface IManufacturerRepository : IRepositoryAsync<Manufacturer>
    {
        Task UpdateAsync(Manufacturer manufacturer);
        Task<PagedResult<Manufacturer>> SearchAsync(SearchManufacturersQuery query, string propertiesToInclude = null);
        Task<Manufacturer> GetByName(string name, string includeProperties = null);
        Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
        Task<Manufacturer> GetById(Guid Id, string includeProperties = null);
    }
}
