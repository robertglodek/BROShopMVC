using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Comment;
using BRO.Domain.Command.Review;
using BRO.Domain.Command.ShoppingCart;
using BRO.Domain.Query.Comment;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Query.Product;
using BRO.Domain.Query.ProductTaste;
using BRO.Domain.Query.Review;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static BRO.Domain.Utilities.Result;

namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public HomeController(IMediator mediator, IMapper mapper, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SearchProductsQuery query)
        {
            if (query.IsDefault())
            {
                var model = new MainPageViewModel();
                model.Bestsellers = await _mediator.QueryAsync(new GetBestsellersQuery());
                model.Latest = await _mediator.QueryAsync(new GetLatestProductsQuery());
                return View("Main",model);
            }
            query.OnlyAvailable = true;
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<ProductDTO>> Fetch(SearchProductsQuery query)
        {
            var viewModel = new PagedResultViewModel<ProductDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new ProductSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }




        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var productDTO = await _mediator.QueryAsync(new GetProductWithDetailsQuery() { Id = id });
            var model = new AddToShoppingCartViewModel()
            {
                Command = new AddShoppingCartItemCommand() { Count=1},
                Product = productDTO,
                Comments = await _mediator.QueryAsync(new SearchCommentsQuery() { ProductId = productDTO.Id}),
                Tastes = productDTO.ProductTastes.Select(n=>new SelectListItem() { Text=n.Taste.Name, Value=n.Id.ToString()})
            };
            if(User.Identity.IsAuthenticated)
            { 
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                model.OwnOpinion = await _mediator.QueryAsync(new GetReviewQuery() { UserId = Guid.Parse(claim.Value), ProductId = id });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Details(AddToShoppingCartViewModel model)
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                if(model.Command.Count>=1)
                {
                    var productTasteInStock = (await _mediator.QueryAsync(new GetProductTasteQuery() { Id = model.Command.ProductTasteId })).InStock;
                    var sessionShoppingCart = HttpContext.Session.GetObject<List<ShoppingCartItemBasicDTO>>(SessionExtensions.SessionShoppingCart);
                    if (model.Command.Count <= productTasteInStock)
                    {
                        if (sessionShoppingCart == null)
                        {
                            sessionShoppingCart = new List<ShoppingCartItemBasicDTO>();
                            sessionShoppingCart.Add(new ShoppingCartItemBasicDTO() { Count = model.Command.Count, ProductTasteId = model.Command.ProductTasteId, Id = Guid.NewGuid() });
                            HttpContext.Session.SetObject(SessionExtensions.SessionShoppingCart, sessionShoppingCart);
                            TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Produkt dodano do koszyka", ActionSuccessfull = true });
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            var element = sessionShoppingCart.FirstOrDefault(n => n.ProductTasteId == model.Command.ProductTasteId);
                            if (element == null)
                            {
                                sessionShoppingCart.Add(new ShoppingCartItemBasicDTO() { Count = model.Command.Count, ProductTasteId = model.Command.ProductTasteId, Id = Guid.NewGuid() });
                                HttpContext.Session.SetObject(SessionExtensions.SessionShoppingCart, sessionShoppingCart);
                                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Produkt dodano do koszyka", ActionSuccessfull = true });
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                if ((model.Command.Count + element.Count) <= productTasteInStock)
                                {
                                    element.Count += model.Command.Count;
                                    HttpContext.Session.SetObject(SessionExtensions.SessionShoppingCart, sessionShoppingCart);
                                    TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Produkt dodano do koszyka", ActionSuccessfull = true });
                                    return RedirectToAction("Index");
                                }
                                else
                                    ModelState.PopulateValidation(new List<Error>(), $"Brak produktu o tym smaku w podanej ilości", "Command.");
                            }
                        }
                    }
                    else
                        ModelState.PopulateValidation(new List<Error>(), $"Brak produktu o tym smaku w podanej ilości", "Command.");
                }
                else
                    ModelState.PopulateValidation(new List<Error>(), $"Minimalna ilość produktu: 1", "Command."); 
            }
            else
            {
                
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                model.Command.ApplicationUserId = Guid.Parse(claim.Value);
                var result = await _mediator.CommandAsync(model.Command); 
                if(result.IsSuccess)
                {
                    TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Produkt dodano do koszyka", ActionSuccessfull = true });
                    return RedirectToAction("Index");
                }
                ModelState.PopulateValidation(result.Errors, result.Message, "Command.");
            }
            var productDTO = await _mediator.QueryAsync(new GetProductWithDetailsQuery() { Id = model.Product.Id });
            model.Command = new AddShoppingCartItemCommand() { Count = 1 };
            model.Product = productDTO;
            model.Comments = await _mediator.QueryAsync(new SearchCommentsQuery() { ProductId = productDTO.Id });
            model.Tastes = productDTO.ProductTastes.Select(n => new SelectListItem() { Text = n.Taste.Name, Value = n.Id.ToString() });
            return View(model);
        }

        #region ApiCalls
        [HttpGet]
        public async Task<IActionResult> ShowMoreComments(SearchCommentsQuery query)
        {
            var result= await _mediator.QueryAsync(query);
            foreach (var item in result.Comments)
                item.ShortDateTimeOffset = item.PublishDateTime.ToString("MM/dd/yyyy");
            return Json(new { success = true, message = "", data=result });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Review(Guid productId,int rating,string content)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var result = await _mediator.CommandAsync(new AddReviewCommand() { ProductId = productId, Content = content, Rating = rating, UserId = Guid.Parse(claim.Value) });
            if (result.IsSuccess)
                return Json(new { success = true, message = "" });
            return Json(new { success = false, message = result.Message });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment(Guid productId, string content)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var result = await _mediator.CommandAsync(new AddCommentCommand() { ProductId = productId, Content = content, UserId = Guid.Parse(claim.Value) });
            if (result.IsSuccess)
                return Json(new { success = true, message = "" });
            return Json(new { success = false, message = result.Message });
        }
        #endregion
    }
}
