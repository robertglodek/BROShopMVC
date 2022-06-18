using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.OrderHeader;
using BRO.Domain.IServices;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.Carrier;
using BRO.Domain.Query.DiscountCode;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Query.Order;
using BRO.Domain.Query.ShoppingCartItem;
using BRO.Domain.Utilities.CustomExceptions;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static BRO.Domain.Utilities.Result;

namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostEnviroment;
        public OrderController(IMediator mediator, IPaymentService paymentService, IMapper mapper, IEmailService emailService, IWebHostEnvironment hostEnviroment)
        {
            _mediator = mediator;
            _paymentService = paymentService;
            _mapper = mapper;
            _emailService = emailService;
            _hostEnviroment = hostEnviroment;
        }

       
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delivery()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var shoppingCartOrderTotal= await _mediator.QueryAsync(new GetShoppingCartTotalQuery() { UserId = Guid.Parse(claim.Value) });
            var viewModel = new OrderCarrierViewModel();          
            viewModel.OrderProductsTotal = await _mediator.QueryAsync(new GetShoppingCartTotalQuery() { UserId = Guid.Parse(claim.Value) });
            viewModel.Carriers = await _mediator.QueryAsync(new GetCarriersQuery());
            if(viewModel.Carriers==null || viewModel.Carriers.Count==0 || viewModel.Carriers.Where(n=>n.IsAvailable==true).Count()<1 )
            {
                throw new Exception($"There are no carriers.");
            }
           
            foreach (var item in viewModel.Carriers)
                item.ActualShippingCost = viewModel.OrderProductsTotal >= item.FreeShippingFromPrice ? 0 : item.ShippingCost;
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Payment(OrderCarrierViewModel model)
        {
            var isDiscount = true;
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var newModel = new OrderPaymentViewModel();
            newModel.Command = new AddOrderCommand(); 
            if (model.CarrierId != default(Guid))
            {
                DiscountCodeDTO discountCode=null;
                if (model.DiscountCode!=null)
                {
                    discountCode = await _mediator.QueryAsync(new GetDiscountCodeQuery() { CodeName = model.DiscountCode });
                    if(discountCode==null)
                    {
                        isDiscount = false;
                        ModelState.PopulateValidation(new List<Error>(), "Podano nieprawidłowy kod zniżkowy");
                    }
                    else if(model.OrderProductsTotal <discountCode.MinimumOrderPrice)
                    {
                        isDiscount = false;
                        ModelState.PopulateValidation(new List<Error>(), $"Łączny koszt produktów dla tego kodu to: {discountCode.MinimumOrderPrice.ToString("C")}");
                    }
                    else if(discountCode.NumberOfUsesLeft <= 0 || discountCode.CodeAvailabilityEnd<DateTimeOffset.Now)
                    {
                        isDiscount = false;
                        ModelState.PopulateValidation(new List<Error>(), $"Kod zniżkowy nie ważny");
                    }
                }
                else
                    isDiscount = false;

                newModel.Carrier = await _mediator.QueryAsync(new GetCarrierQuery() { Id = model.CarrierId });
                var userDTO = await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = Guid.Parse(claim.Value) });
                newModel.DiscountCode= discountCode;
                newModel.Command.Street = userDTO.Street;
                newModel.Command.PostalCode = userDTO.PostalCode;
                newModel.Command.City = userDTO.City;
                newModel.Command.PhoneNumber = userDTO.PhoneNumber;
                newModel.Command.OrderProductsTotal = model.OrderProductsTotal;   
                newModel.Command.OrderShippingCost = model.OrderShippingCost;   
                newModel.Command.CarrierId = newModel.Carrier.Id;
                if (isDiscount == true)
                {
                    newModel.Command.OrderProductsTotalAfterDiscount = newModel.Command.OrderProductsTotal - newModel.Command.OrderProductsTotal * ((double)discountCode.DiscountInPercent / 100);
                    newModel.Command.DiscountCodeId = discountCode.Id;
                }     
                else
                    newModel.Command.OrderProductsTotalAfterDiscount = null;
                return View("Payment", newModel);
            }
            else
            {
                model.OrderProductsTotal = await _mediator.QueryAsync(new GetShoppingCartTotalQuery() { UserId = Guid.Parse(claim.Value) });
                model.Carriers = await _mediator.QueryAsync(new GetCarriersQuery());
                if (model.Carriers == null || model.Carriers.Count == 0)
                {
                    throw new NotFoundException($"There are no carriers.");
                }
                foreach (var item in model.Carriers)
                    item.ActualShippingCost = model.OrderProductsTotal >= item.FreeShippingFromPrice ? 0 : item.ShippingCost;
                ModelState.PopulateValidation(new List<Error>(), "Należy wybrać metodę dostawy");
                return View("Delivery", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSummary(OrderPaymentViewModel model)
        {

            Guid orderId;
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            orderId = Guid.NewGuid();
            model.Command.UserId = Guid.Parse(claim.Value);
            model.Command.Id = orderId;

            var userEmail = (await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = Guid.Parse(claim.Value) })).Email;

            var result = await _mediator.CommandAsync(model.Command);
            if (result.IsSuccess)
            {
                var orderDetailsLink = Url.Action("OrderDetails", "UserSettings", new { area = "Identity",  Id = orderId }, Request.Scheme);
                var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                var subject = "Potwierdzenie zamówienia";
                string htmlBody = "";
                var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                   + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "OrderConfirmation.html";
                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                var order = await _mediator.QueryAsync(new GetOrderQuery { Id = orderId });

                var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                var messageBody = string.Format(htmlBody, logoPath,order.OrderNumber.ToString(),orderDetailsLink, homeActionLink);
                await _emailService.SendEmailAsync(userEmail, subject, messageBody, "");

                if (model.Command.PaymentMethod == BRO.Domain.Utilities.StaticDetails.PaymentMethods.PaymendMethodOnline)
                {          
                    var urlSuccess = Url.Action("PaymentSuccess", "Order", new { area = "Customer", orderId = orderId }, Request.Scheme) + "&session_id={CHECKOUT_SESSION_ID}";
                    var urlCancel = Url.Action("PaymentCancel", "Order", new { area = "Customer", orderId = orderId }, Request.Scheme) + "&session_id={CHECKOUT_SESSION_ID}";
                    var paymentUrl = (await _paymentService.Pay(order.OrderProductsTotal +order.OrderShippingCost, userEmail, urlSuccess, urlCancel)).Message;
                    Response.Headers.Add("Location", paymentUrl);
                    return new StatusCodeResult(303);
                }
                else
                    return View("OrderConfirmation");
            }
            if(model.Command.OrderProductsTotalAfterDiscount!=null)
            {
                model.DiscountCode = await _mediator.QueryAsync(new GetDiscountCodeQuery() { Id=model.Command.DiscountCodeId.Value });
            }
            model.Carrier = await _mediator.QueryAsync(new GetCarrierQuery() { Id = model.Command.CarrierId });
            
            ModelState.PopulateValidation(result.Errors, result.Message,"Command.");
            return View("Payment", model);
        }


        [HttpPost]
        public async Task<IActionResult> PayAgain(Guid id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var order = await _mediator.QueryAsync(new GetOrderQuery() { Id = id });
            var userEmail = (await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = Guid.Parse(claim.Value) })).Email;

            var urlSuccess = Url.Action("PaymentSuccess", "Order", new { area = "Customer", orderId = id }, Request.Scheme) + "&session_id={CHECKOUT_SESSION_ID}";
            var urlCancel = Url.Action("PaymentCancel", "Order", new { area = "Customer", orderId = id }, Request.Scheme) + "&session_id={CHECKOUT_SESSION_ID}";
            var paymentUrl = (await _paymentService.Pay(order.OrderProductsTotal + order.OrderShippingCost, userEmail, urlSuccess, urlCancel)).Message;
            Response.Headers.Add("Location", paymentUrl);
            return new StatusCodeResult(303);          
        }



        public async Task<IActionResult> PaymentSuccess([FromQuery] Guid orderId, [FromQuery] string session_id)
        {

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var orderDetailsLink = Url.Action("OrderDetails", "UserSettings", new { area = "Identity", Id = orderId }, Request.Scheme);
            var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
            var subject = "Potwierdzenie zapłaty";
            string htmlBody = "";
            var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
               + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "PaymentConfirmation.html";
            using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
            {
                htmlBody = streamReader.ReadToEnd();
            }
            var order = await _mediator.QueryAsync(new GetOrderQuery { Id = orderId });
            var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
            var messageBody = string.Format(htmlBody, logoPath, order.OrderNumber.ToString(), orderDetailsLink, homeActionLink);
            await _emailService.SendEmailAsync(identity.FindFirst(ClaimTypes.Email).Value, subject, messageBody, "");
            var paymentIntentId = await _paymentService.ReturnPaymentId(session_id);
            var command = new EditOrderCommand();
            _mapper.Map(order, command);
            command.TransactionId = paymentIntentId.Message;
            command.PaymentStatus = PaymentStatus.PaymentStatusApproved;
            command.PaymentDate = DateTimeOffset.Now;

            await _mediator.CommandAsync(command);

            return View();
        }



        public async Task<IActionResult> PaymentCancel([FromQuery] Guid orderId)
        {
           
            var order = await _mediator.QueryAsync(new GetOrderQuery() { Id = orderId });
            var command = new EditOrderCommand();
            _mapper.Map(order, command);
            command.PaymentStatus = PaymentStatus.PaymentStatusCanceled;

            await _mediator.CommandAsync(command);

            return View();
        }



    }
}
