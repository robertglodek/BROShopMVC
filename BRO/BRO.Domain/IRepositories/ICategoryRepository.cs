using BRO.Domain.Entities;
using BRO.Domain.Query.Category;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BRO.Domain.IRepositories
{
    public interface ICategoryRepository:IRepositoryAsync<Category>
    {
       Task UpdateAsync(Category category);
       Task<PagedResult<Category>> SearchAsync(SearchCategoriesQuery query, string propertiesToInclude = null);
       Task<Category> GetByName(string name, string includeProperties = null);
       Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
       Task<Category> GetById(Guid Id, string includeProperties = null);

    }
}
