using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IProductRepository:IRepositoryAsync<Product>
    {
        Task UpdateAsync(Product product);
        Task<PagedResult<Product>> SearchAsync(SearchProductsQuery query, string propertiesToInclude = null);
        public Task<List<ProductTaste>> GetBestsellersAsync(int count);
        public Task<List<Product>> GetLatestAsync(int count);
        Task<Product> GetByName(string name, string includeProperties = null);
        Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
        Task<Product> GetByIdWithDetails(Guid id);
        Task<Product> GetById(Guid Id, string includeProperties = null);
    }
}
