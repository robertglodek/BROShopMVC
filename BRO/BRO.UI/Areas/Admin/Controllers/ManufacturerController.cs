using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Manufacturer;
using BRO.Domain.Query.DTO.Manufacturer;
using BRO.Domain.Query.Manufacturer;
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

    public class ManufacturerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ManufacturerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchManufacturersQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<ManufacturerDTO>> Fetch(SearchManufacturersQuery query)
        {
            var viewModel = new PagedResultViewModel<ManufacturerDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new ManufacturerSortingRules(), query.SortBy);
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
        public async Task<IActionResult> Add(AddManufacturerCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodano dostawcę", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }   
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryDTO = await _mediator.QueryAsync(new GetManufacturerQuery() { Id = id });
            var model = _mapper.Map<EditManufacturerCommand>(categoryDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditManufacturerCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano producenta", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }  
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteManufacturerCommand() { Id = id });
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto producenta" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));
        }
    }
}
