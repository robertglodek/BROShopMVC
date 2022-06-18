using AutoMapper;
using BRO.Domain;
using BRO.Domain.Query.DTO.Review;
using BRO.Domain.Query.Review;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = Roles.RoleAdmin+","+Roles.RoleEmployee)]
    public class ReviewController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
       
        public ReviewController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
           
        }
        public async Task<IActionResult> Index(SearchReviewsQuery query)
        {

            return View(await Fetch(query));

        }
        private async Task<PagedResultViewModel<ReviewDTO>> Fetch(SearchReviewsQuery query)
        {
            var viewModel = new PagedResultViewModel<ReviewDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new ReviewSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid userId,Guid productId)
        {
            var reviewDto = await _mediator.QueryAsync(new GetReviewQuery() { ProductId = productId, UserId = userId });      
            return View(reviewDto);
        }
    }
}
