using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.Review;
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
    public class ReviewRepository : RepositoryAsync<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PagedResult<Review>> SearchAsync(SearchReviewsQuery query, string propertiesToInclude = null)
        {
            IQueryable<Review> baseQuery = _db.Reviews;
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new ReviewSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
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
                var columnsSelectorsNumber = new ReviewSortingRules().SortDictionaryNumber;
                var columnsSelectorsDate = new ReviewSortingRules().SortDictionaryDate;
                if (columnsSelectorsNumber.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsNumber[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else if (columnsSelectorsDate.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsDate[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
            }
            var reviews = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<Review>(reviews, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;
        }
        public IQueryable<Review> ReturnSorted<T>(string sortDirection, Expression<Func<Review, T>> expression, IQueryable<Review> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);

            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }

        public async Task<Review> GetByProductIdAndUserId(Guid userId, Guid productId, string includeProperties=null)
        {
            IQueryable<Review> baseQuery = _db.Reviews;
            baseQuery = baseQuery.Where(n => n.UserId==userId && n.ProductId==productId);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    baseQuery = baseQuery.Include(includeProp);
                }
            }
            return await baseQuery.FirstOrDefaultAsync();
        }

        
    }
}
