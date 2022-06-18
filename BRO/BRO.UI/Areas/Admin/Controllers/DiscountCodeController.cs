using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.DiscountCode;
using BRO.Domain.Query.DiscountCode;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.RoleAdmin)]

    public class DiscountCodeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DiscountCodeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchDiscountCodesQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<DiscountCodeDTO>> Fetch(SearchDiscountCodesQuery query)
        {
            var viewModel = new PagedResultViewModel<DiscountCodeDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new DiscountCodeSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddDiscountCodeCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano kod", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }   
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var discountCodeDTO = await _mediator.QueryAsync(new GetDiscountCodeQuery() { Id = id });
            var model = _mapper.Map<EditDiscountCodeCommand>(discountCodeDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditDiscountCodeCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano kod", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }  
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteDiscountCodeCommand() { Id = id });
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto kod" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));
        }
    }
}
