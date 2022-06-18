using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Category;
using BRO.Domain.Query.Category;
using BRO.Domain.Query.DTO.Category;
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
 
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CategoryController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchCategoriesQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<CategoryDTO>> Fetch(SearchCategoriesQuery query)
        {
            var viewModel = new PagedResultViewModel<CategoryDTO>();
            viewModel.Result= await _mediator.QueryAsync(query);
            viewModel.PageSizes= Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new CategorySortingRules(), query.SortBy);
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
        public async Task<IActionResult> Add(AddCategoryCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano kategorię", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryDTO=await _mediator.QueryAsync(new GetCategoryQuery() { Id = id });
            var model = _mapper.Map<EditCategoryCommand>(categoryDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano kategorię", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }  
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteCategoryCommand() { Id = id });
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess?"Pomyślnie usunięto kategorię": result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));

        }
    }
}
