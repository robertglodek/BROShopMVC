using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.ApplicationUser;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.Role;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Employee.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.RoleAdmin)]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(SearchApplicationUsersQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<ApplicationUserDTO>> Fetch(SearchApplicationUsersQuery query)
        {
            var viewModel = new PagedResultViewModel<ApplicationUserDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new ApplicationUserSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _mediator.QueryAsync(new GetRolesQuery());
            var model = new RegisterUserViewModel()
            {
                Command = new AddApplicationUserCommand(),
                Roles = roles.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = n.Name, Value = n.Name })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterUserViewModel model)
        {
            var result = await _mediator.CommandAsync(model.Command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie dodanu użytkownika", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }

            var roles = await _mediator.QueryAsync(new GetRolesQuery());
            model.Roles = roles.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = n.Name, Value = n.Name });
            ModelState.PopulateValidation(result.Errors, result.Message, "Command.");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Lockout(Guid id)
        {
            var userDTO = await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = id });
            var model = _mapper.Map<LockoutApplicationuserCommand>(userDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Lockout(LockoutApplicationuserCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie wykonano operację", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
    }
}
