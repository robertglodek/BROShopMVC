using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.Comment;
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
    public class CommentRepository : RepositoryAsync<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CommentsPagedResult<Comment>> SearchAsync(SearchCommentsQuery query, string propertiesToInclude = null)
        {
            IQueryable<Comment> baseQuery = _db.Comments;
            baseQuery = baseQuery.Where(n => n.ProductId == query.ProductId && n.Allowed==true );
            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            var comments = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new CommentsPagedResult<Comment>(comments, totalElementsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public async Task<PagedResult<Comment>> SearchDetailsAsync(SearchCommentsDetailsQuery query, string propertiesToInclude = null)
        {

            IQueryable<Comment> baseQuery = _db.Comments;



            if (propertiesToInclude != null)
            {
                foreach (var includeProp in propertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    baseQuery = baseQuery.Include(includeProp);
                }

            }
        

            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new CommentSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
                if (searchSelectors.ContainsKey(query.SearchName.ToLower()))
                {
                    var selectedSearch = searchSelectors[query.SearchName.ToLower()];
                    baseQuery = baseQuery.Where(selectedSearch);
                }
                else
                {
                    throw new NotFoundException($"SearchName with value: {query.SearchName} does not exist");
                }


            }

            if (query.SortBy != "--")
            {
                var sortPhrase = query.SortBy.Split(":")[0];
                var sortDir = query.SortBy.Split(":")[1];

                var columnsSelectorsDate = new CommentSortingRules().SortDictionaryDate;
                var columnsSelectorsBool = new CommentSortingRules().SortDictionaryBool;

                if (columnsSelectorsDate.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsDate[sortPhrase];
                    baseQuery = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                }
                else if (columnsSelectorsBool.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectorsBool[sortPhrase];
                    baseQuery = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                }
                else
                {
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
                }



            }


            var comments = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();

            var totalElementsCount = await baseQuery.CountAsync();

            var result = new PagedResult<Comment>(comments, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);

            return result;

        }
        public IQueryable<Comment> ReturnSorted<T>(string sortDirection, Expression<Func<Comment, T>> expression, IQueryable<Comment> comments)
        {
            if (sortDirection == "asc")
                return comments = comments.OrderBy(expression);

            else if (sortDirection == "desc")
                return comments = comments.OrderByDescending(expression);
            return null;
        }

        public async Task<Comment> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<Comment> query = _db.Comments;
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
