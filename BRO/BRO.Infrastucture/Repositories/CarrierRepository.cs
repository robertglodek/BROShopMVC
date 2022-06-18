using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.Carrier;
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
    public class CarrierRepository:RepositoryAsync<Carrier>,ICarrierRepository
    {
        private readonly ApplicationDbContext _db;
        public CarrierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PagedResult<Carrier>> SearchAsync(SearchCarriersQuery query, string propertiesToInclude = null)
        {
            IQueryable<Carrier> baseQuery = _db.Carriers;

            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";

                var searchSelectors = new CarrierSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
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
                var columnsSelectors = new CarrierSortingRules().SortDictionary;
                if (columnsSelectors.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectors[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
            }
            var carriers = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<Carrier>(carriers, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;
        }
        public IQueryable<Carrier> ReturnSorted<T>(string sortDirection, Expression<Func<Carrier, T>> expression, IQueryable<Carrier> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);
            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }

        public async Task<Carrier> GetByName(string name, string includeProperties = null)
        {
            IQueryable<Carrier> baseQuery = _db.Carriers;
            baseQuery = baseQuery.Where(n => n.Name.ToLower() == name.ToLower());
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
            IQueryable<Carrier> baseQuery = _db.Carriers;
            var result = await baseQuery.FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower() && n.Id != id);
            return result == null ? false : true;
        }

        public async Task<Carrier> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<Carrier> query = _db.Carriers;
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
