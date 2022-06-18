using BRO.Domain.Entities;
using BRO.Domain.Query.DiscountCode;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IDiscountCodeRepository : IRepositoryAsync<DiscountCode>
    {
        Task UpdateAsync(DiscountCode discountCode);
        Task<PagedResult<DiscountCode>> SearchAsync(SearchDiscountCodesQuery query, string propertiesToInclude = null);
        Task<DiscountCode> GetByName(string name, string includeProperties = null);
        Task<bool> CheckIfNameAlreadyExists(Guid id, string name);
        Task<DiscountCode> GetById(Guid Id, string includeProperties = null);
    }
}
