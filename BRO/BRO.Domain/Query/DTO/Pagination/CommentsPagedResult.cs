using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.Pagination
{
    public class CommentsPagedResult<T>
    {

        public List<T> Comments { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalElementsCount { get; set; } 
        public CommentsPagedResult(List<T> elements, int totalElementsCount, int pageSize, int pageNumber)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalElementsCount / (decimal)pageSize);
            int currentPage = pageNumber;

            TotalElementsCount = totalElementsCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            Comments = elements;
        }
    }
}
