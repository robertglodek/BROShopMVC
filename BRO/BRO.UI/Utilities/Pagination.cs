using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.Utilities
{
    public static class Pagination
    {
        public static IEnumerable<SelectListItem> GetPageSizesAndSelected(int selected)
        {
            var sizes = new List<SelectListItem>();
            foreach (int size in PagedResultSizes.GetAllowedSizes())
            {
                if(size==selected)
                {
                    sizes.Add(new SelectListItem() { Value=size.ToString(), Text= size.ToString(), Selected=true });
                    continue;
                }
                sizes.Add(new SelectListItem() { Value = size.ToString(), Text = size.ToString() });
            }
            return sizes;
        }
        public static IEnumerable<SelectListItem> GetSortRulesAndSelected<T>(ISortingRules<T> rules,string rule="--")
        {
            var selectList= new List<SelectListItem>();
            if (rule == "--")
                selectList.Add(new SelectListItem() { Text = "--", Value = $"--", Selected = true });
            else
                selectList.Add(new SelectListItem() { Text = "--", Value = $"--", Selected = false });
            foreach (string item in rules.SortList)
            {
                var searchingRule = rule.ToLower().Split(":");
                
                if (searchingRule[0]==item)
                {
                    if(searchingRule[1]=="asc")
                    {
                        selectList.Add(new SelectListItem() { Text = $"{item} : rosnąco", Value = $"{item}:asc", Selected = true }) ;
                        selectList.Add(new SelectListItem() { Text = $"{item} : malejąco", Value = $"{item}:desc", Selected = false });
                    }
                    else if (searchingRule[1] == "desc")
                    {
                        selectList.Add(new SelectListItem() { Text = $"{item} : malejąco", Value = $"{item}:desc", Selected = true });
                        selectList.Add(new SelectListItem() { Text = $"{item} : rosnąco", Value = $"{item}:asc", Selected = false });
                    }
                }
                else
                {
                    selectList.Add(new SelectListItem() { Text = $"{item} : rosnąco", Value = $"{item}:asc" });
                    selectList.Add(new SelectListItem() { Text = $"{item} : malejąco", Value = $"{item}:desc" });
                }
            }
            return selectList;
        }


    }
}
