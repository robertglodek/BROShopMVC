using BRO.Domain.Query.DTO.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class PagedResultViewModel<T>
    {
        public PagedResult<T> Result { get; set; }
        public string SearchName { get; set; }
        public string SearchValue { get; set; }
        public IEnumerable<SelectListItem> PageSizes { get; set; }
        public IEnumerable<SelectListItem> Sorting { get; set; }

    }
}
