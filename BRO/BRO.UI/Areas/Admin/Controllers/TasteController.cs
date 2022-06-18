using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Taste;
using BRO.Domain.Query.DTO.Taste;
using BRO.Domain.Query.Taste;
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
    public class TasteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TasteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchTastesQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<TasteDTO>> Fetch(SearchTastesQuery query)
        {
            var viewModel = new PagedResultViewModel<TasteDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new TasteSortingRules(), query.SortBy);
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
        public async Task<IActionResult> Add(AddTasteCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano smak", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            } 
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryDTO = await _mediator.QueryAsync(new GetTasteQuery() { Id = id });
            var model = _mapper.Map<EditTasteCommand>(categoryDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTasteCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano smak", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }  
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteTasteCommand() { Id = id });
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto smak" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));
        }
    }
}
