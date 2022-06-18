using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query
{
    public class SearchQuery
    {
        public string SearchName { get; set; }
        public string SearchValue { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = BRO.Domain.Utilities.StaticDetails.PagedResultSizes.GetAllowedSizes().First();
        public string SortBy { get; set; } = "--";
    }
}
