using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.Pagination
{
    public class PagedResult<T>
    {
        public List<T> PageElements { get; private set; }
        public int ElementsFrom { get; private set; }
        public int ElementsTo { get; private set; }
        public int TotalElementsCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public string SortBy { get; private set; }
        public PagedResult(List<T> elements, int totalElementsCount, int pageSize, int pageNumber,string sortBy)
        {
            SortBy = sortBy;
            int totalPages = (int)Math.Ceiling((decimal)totalElementsCount / (decimal)pageSize);
            int currentPage = pageNumber;
            int startPage = currentPage - 2;
            int endPage = currentPage + 2;
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 5)
                    startPage = endPage - 4;
            }
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;     
            EndPage = endPage;
            PageElements = elements;
            TotalElementsCount = totalElementsCount;
            ElementsFrom = pageSize * (pageNumber - 1) + 1;
            ElementsTo = ElementsFrom + pageSize - 1;
        }
    }
}
