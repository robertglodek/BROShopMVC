using BRO.Domain;
using BRO.Domain.Command.ShoppingCart;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Query.ProductTaste;
using BRO.Domain.Query.ShoppingCartItem;
using BRO.UI.Extensions;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
          
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ShoppingCartItemDTO> shoppingCartItems=new List<ShoppingCartItemDTO>();
            if (!User.Identity.IsAuthenticated)
            {
                var sessionShoppingCart = HttpContext.Session.GetObject<List<ShoppingCartItemBasicDTO>>(SessionExtensions.SessionShoppingCart);
                if(sessionShoppingCart!=null)
                    shoppingCartItems = await _mediator.QueryAsync(new GetShoppingCartItemsForItemsQuery() { ProductTasteIds = sessionShoppingCart });
            }
            else
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCartItems = await _mediator.QueryAsync(new GetShoppingCartItemsQuery() { ApplicationUserId = Guid.Parse(claim.Value) });
            }
            var viewModel = new ShoppingCartViewModel() { Cart = shoppingCartItems };
            foreach (var item in viewModel.Cart)
            {
                item.Price = item.ProductTaste.Product.IsDiscount == true ? item.ProductTaste.Product.PromotionalPrice : item.ProductTaste.Product.RegularPrice;
                viewModel.OrderProductsTotal += item.Price * item.Count;
            }
            return View(viewModel);
        }
        #region Api Calls
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var shoppingCartItem = await _mediator.QueryAsync(new GetShoppingCartItemQuery() { Id = id });
                shoppingCartItem.Price = shoppingCartItem.ProductTaste.Product.IsDiscount == true ? shoppingCartItem.ProductTaste.Product.PromotionalPrice : shoppingCartItem.ProductTaste.Product.RegularPrice;

                var result = await _mediator.CommandAsync(new DeleteShoppingCartItemCommand() { Id = id });
                if (result.IsSuccess)
                    return Json(new { success = true, message = "", item = shoppingCartItem });
                return Json(new { success = false, message = result.Message });
            }
            else
            {
                var sessionShoppingCart = HttpContext.Session.GetObject<List<ShoppingCartItemBasicDTO>>(SessionExtensions.SessionShoppingCart);
                var productToRemove = sessionShoppingCart.FirstOrDefault(n => n.Id == id);
                var productTaste = await _mediator.QueryAsync(new GetProductTasteWithDetailsQuery() { Id = productToRemove.ProductTasteId });
                var shoppingCartItem = new ShoppingCartItemDTO()  
                {
                    Price = productTaste.Product.IsDiscount == true ? productTaste.Product.PromotionalPrice : productTaste.Product.RegularPrice,
                    Count = productToRemove.Count,
                    ProductTaste = productTaste,
                    ProductTasteId = productTaste.Id
                };
                if (!sessionShoppingCart.Remove(productToRemove))
                    return Json(new { success = false, message = "Operacja nie powiodła się." });
                else
                {
                    HttpContext.Session.SetObject(SessionExtensions.SessionShoppingCart, sessionShoppingCart);
                    return Json(new { success = true, message = "", item = shoppingCartItem });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditQuantity(Guid id,int count)
        {
            if(User.Identity.IsAuthenticated)
            {
                var result = await _mediator.CommandAsync(new EditShoppingCartItemCommand() { ShoppingCartItemId = id, Count = count });
                if (result.IsSuccess)
                {
                    var shoppingCartItem = await _mediator.QueryAsync(new GetShoppingCartItemQuery() { Id = id });
                    shoppingCartItem.Price = shoppingCartItem.ProductTaste.Product.IsDiscount == true ? shoppingCartItem.ProductTaste.Product.PromotionalPrice : shoppingCartItem.ProductTaste.Product.RegularPrice;
                    return Json(new { success = true, message = "", item = shoppingCartItem });
                }
                return Json(new { success = false, message = result.Message });
            }
            else
            {     
                var sessionShoppingCart = HttpContext.Session.GetObject<List<ShoppingCartItemBasicDTO>>(SessionExtensions.SessionShoppingCart);
                var shoppingCartElementToChange = sessionShoppingCart.FirstOrDefault(n => n.Id == id);
                var productTaste= await _mediator.QueryAsync(new GetProductTasteWithDetailsQuery() { Id = shoppingCartElementToChange.ProductTasteId });
                if(productTaste.InStock>=count)
                {
                    shoppingCartElementToChange.Count = count;
                    HttpContext.Session.SetObject(SessionExtensions.SessionShoppingCart, sessionShoppingCart);
                    var shoppingCartItem = new ShoppingCartItemDTO()
                    {
                        Price = productTaste.Product.IsDiscount == true ? productTaste.Product.PromotionalPrice : productTaste.Product.RegularPrice,
                        Count = count,
                        ProductTaste = productTaste,
                        ProductTasteId = productTaste.Id
                    };
                    return Json(new { success = true, message = "", item = shoppingCartItem });
                }
                else
                    return Json(new { success = false, message = $"Brak produktu o tym smaku w podanej ilosci" });
            }
        }
        #endregion
    }
}
