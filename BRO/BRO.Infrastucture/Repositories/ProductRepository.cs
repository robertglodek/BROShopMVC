using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.Product;
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
    public class ProductRepository : RepositoryAsync<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       public async Task<PagedResult<Product>> SearchAsync(SearchProductsQuery query, string propertiesToInclude = null)
       {
            IQueryable<Product> baseQuery = _db.Products;
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if(query.OnlyAvailable==true)
                baseQuery = baseQuery.Where(n => n.Availability == true);
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new ProductSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
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

                var columnsSelectors = new ProductSortingRules().SortDictionary;
                var columnsSelectorsNumber = new ProductSortingRules().SortDictionaryNumber;

                if (columnsSelectors.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectors[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else if(columnsSelectorsNumber.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsNumber[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
            }
            var products = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<Product>(products, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;

        }



        public IQueryable<Product> ReturnSorted<T>(string sortDirection, Expression<Func<Product, T>> expression, IQueryable<Product> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);
            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }
       

        public async Task<List<ProductTaste>> GetBestsellersAsync(int count)
        {
            IQueryable<ProductTaste> baseQuery = _db.ProductTastes;
            baseQuery = baseQuery.Include(n => n.Product).ThenInclude(n => n.Reviews).Include(n => n.Product).ThenInclude(n => n.Manufacturer).Include(n => n.OrderDetails).Include(n=>n.Taste);
            var products = await baseQuery.OrderByDescending(n => n.OrderDetails.Sum(n => n.Count)).Where(n => n.OrderDetails.Count() > 0 && n.Product.Availability).Take(count).ToListAsync();

            return products;
        }
        public async Task<List<Product>> GetLatestAsync(int count)
        {
            IQueryable<Product> baseQuery = _db.Products;
            baseQuery = baseQuery.Include(n => n.Reviews).Include(n => n.Category).Include(n => n.Manufacturer);
            var products = await baseQuery.OrderBy(n => n.ProductAddDate).Where(n =>n.Availability).Take(count).ToListAsync();
            return products;
        }


        
        public async Task<Product> GetByName(string name, string includeProperties = null)
        {
            IQueryable<Product> baseQuery = _db.Products;
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
            IQueryable<Product> baseQuery = _db.Products;
            var result = await baseQuery.FirstOrDefaultAsync(n => n.Name.ToLower() == name.ToLower() && n.Id != id);
            return result == null ? false : true;
        }

        public async Task<Product> GetByIdWithDetails(Guid id)
        {
            IQueryable<Product> baseQuery = _db.Products;
            baseQuery = baseQuery.Where(n => n.Id== id);
            baseQuery = baseQuery.Include(n => n.Category).Include(n => n.Reviews).Include(n => n.Manufacturer)
                .Include(n => n.ProductTastes).ThenInclude(n => n.Taste).Include(n => n.ProductTastes).ThenInclude(n=>n.OrderDetails);
            return await baseQuery.FirstOrDefaultAsync();

        }

        public async Task<Product> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<Product> query = _db.Products;
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
