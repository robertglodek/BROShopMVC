using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.Order;
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
   
    public class OrderHeaderRepository : RepositoryAsync<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<OrderHeader> GetByIdWithDetails(Guid id)
        {
            IQueryable<OrderHeader> query = _db.Orders;
            query = query.Where(n => n.Id == id);
            query = query.Include(n=>n.User).Include(n=>n.Carrier).Include(n => n.OrderDetails).ThenInclude(n => n.ProductTaste).
                ThenInclude(n => n.Product).Include(n => n.OrderDetails).ThenInclude(n => n.ProductTaste).ThenInclude(n => n.Taste).Include(n=>n.OrderBill).Include(n=>n.DiscountCode);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<PagedResult<OrderHeader>> SearchAsync(SearchOrdersQuery query, string propertiesToInclude = null)
        {
            IQueryable<OrderHeader> baseQuery = _db.Orders;
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new OrderSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
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
                var columnsSelectorsNumber = new OrderSortingRules().SortDictionaryNumber;
                var columnsSelectorsDate = new OrderSortingRules().SortDictionaryDate;
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
            var orders = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<OrderHeader>(orders, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;
        }
        public IQueryable<OrderHeader> ReturnSorted<T>(string sortDirection, Expression<Func<OrderHeader, T>> expression, IQueryable<OrderHeader> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);
            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }
        public async new Task UpdateAsync(OrderHeader product)
        {
            _db.Orders.Update(product).Property(x=>x.OrderNumber).IsModified=false;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllForUser(Guid userId)
        {
            IQueryable<OrderHeader> baseQuery = _db.Orders;
            baseQuery = baseQuery.Where(n => n.UserId==userId);
            return await baseQuery.ToListAsync();
        }

        public async Task<OrderHeader> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<OrderHeader> query = _db.Orders;
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
