using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DiscountCode;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities.CustomExceptions;
using BRO.Domain.Utilities.PaginationSearchByRules;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Infrastructure.Repositories;
using BRO.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Repositories
{
    public class DiscountCodeRepository : RepositoryAsync<DiscountCode>, IDiscountCodeRepository
    {
        private readonly ApplicationDbContext _db;
        public DiscountCodeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PagedResult<DiscountCode>> SearchAsync(SearchDiscountCodesQuery query, string propertiesToInclude = null)
        {
            IQueryable<DiscountCode> baseQuery = _db.DiscountCodes;
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new DiscountCodeSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
                if (searchSelectors.ContainsKey(query.SearchName.ToLower()))
                {
                    var selectedSearch = searchSelectors[query.SearchName.ToLower()];
                    baseQuery = baseQuery.Where(selectedSearch);
                }
                else
                    throw new NotFoundException($"SearchName with value: {query.SearchName} does not exist");
            }
            if (query.SortBy != "--")
            {
                var sortPhrase = query.SortBy.Split(":")[0];
                var sortDir = query.SortBy.Split(":")[1];
                var columnsSelectors = new DiscountCodeSortingRules().SortDictionary;
                var columnsSelectorsNumber = new DiscountCodeSortingRules().SortDictionaryNumber;
                if (columnsSelectors.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectors[sortPhrase];
                    var sortingResult= ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if(sortingResult!=null)
                        baseQuery = sortingResult;
                }
                else if (columnsSelectorsNumber.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsNumber[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
            }
            var discountCodes = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<DiscountCode>(discountCodes, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;
        }
        public IQueryable<DiscountCode> ReturnSorted<T>(string sortDirection, Expression<Func<DiscountCode, T>> expression, IQueryable<DiscountCode> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);
            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }


        public async Task<DiscountCode> GetByName(string name, string includeProperties = null)
        {
            IQueryable<DiscountCode> baseQuery = _db.DiscountCodes;
            baseQuery = baseQuery.Where(n => n.CodeName.ToLower() == name.ToLower());
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    baseQuery = baseQuery.Include(includeProp);
                }
            }
            return await baseQuery.FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfNameAlreadyExists(Guid id, string name)
        {
            IQueryable<DiscountCode> baseQuery = _db.DiscountCodes;
            var result = await baseQuery.FirstOrDefaultAsync(n => n.CodeName.ToLower() == name.ToLower() && n.Id != id);
            return result == null ? false : true;
        }

        public async Task<DiscountCode> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<DiscountCode> query = _db.DiscountCodes;
            query = query.Where(n => n.Id == Id);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
