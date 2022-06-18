using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.Taste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BRO.Domain.IRepositories
{
    public interface ITasteRepository:IRepositoryAsync<Taste>
    {
        Task UpdateAsync(Taste category);
        Task<PagedResult<Taste>> SearchAsync(SearchTastesQuery query, string propertiesToInclude = null);
        Task<Taste> GetByName(string name, string includeProperties = null);
        Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
        Task<Taste> GetById(Guid Id, string includeProperties = null);
    }
}
