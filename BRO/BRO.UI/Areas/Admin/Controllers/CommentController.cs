using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Comment;
using BRO.Domain.Query.Comment;
using BRO.Domain.Query.DTO.Comment;
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
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CommentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
           
        }
        [HttpGet]
        public async Task<IActionResult> Index(SearchCommentsDetailsQuery query)
        {
            return View(await Fetch(query));
        }

        private async Task<PagedResultViewModel<CommentDTO>> Fetch(SearchCommentsDetailsQuery query)
        {
            var viewModel = new PagedResultViewModel<CommentDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new CommentSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var commentDto = await _mediator.QueryAsync(new GetCommentQuery() { Id=id });
            var model = _mapper.Map<EditCommentCommand>(commentDto);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano komentarz", ActionSuccessfull = true });
                return RedirectToAction(nameof(Index));
            }   
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.CommandAsync(new DeleteCommentCommand() { Id = id });
            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.IsSuccess ? "Pomyślnie usunięto komentarz" : result.Message, ActionSuccessfull = result.IsSuccess });
            return RedirectToAction(nameof(Index));
        }
    }
}
