using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.OrderHeader;
using BRO.Domain.IServices;
using BRO.Domain.Query.DTO.OrderHeader;
using BRO.Domain.Query.Order;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = Roles.RoleAdmin+","+Roles.RoleEmployee)]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IEmailService _emailService;

        public OrderController(IMediator mediator, IMapper mapper, IEmailService emailService, IWebHostEnvironment hostEnviroment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _hostEnviroment = hostEnviroment;
            _emailService = emailService;

        }
        public async Task<IActionResult> Index(SearchOrdersQuery query)
        {
            return View(await Fetch(query));
        }
        private async Task<PagedResultViewModel<OrderHeaderDTO>> Fetch(SearchOrdersQuery query)
        {
            var viewModel = new PagedResultViewModel<OrderHeaderDTO>();
            viewModel.Result = await _mediator.QueryAsync(query);
            viewModel.PageSizes = Pagination.GetPageSizesAndSelected(query.PageSize);
            viewModel.Sorting = Pagination.GetSortRulesAndSelected(new OrderSortingRules(), query.SortBy);
            viewModel.SearchName = query.SearchName;
            viewModel.SearchValue = query.SearchValue;
            return viewModel;
        }
        public async Task<IActionResult> Details(Guid id)
        {
            return await FetchDetails(id);
        }
        public async Task<IActionResult> FetchDetails(Guid id)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var model = new OrderDetailsViewModel()
            {
                OrderDetails = orderDTO.OrderDetails,
                Code = orderDTO.DiscountCode,
                Command = _mapper.Map<EditOrderCommand>(orderDTO),
                UserEmail = orderDTO.User.Email,
                Carrier = orderDTO.Carrier.Name,
                OrderBill = orderDTO.OrderBill
            };
            
            return View("Details",model);
        }

        [HttpGet]
        public async Task<IActionResult> Delivered(Guid id, string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusDelivered;
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                var orderDetailsLink = Url.Action("OrdersHistory", "UserSettings", new { area = "Identity" }, Request.Scheme);
                var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                var subject = "Potwierdzenie dostarczenia zamówienia";
                string htmlBody = "";
                var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                   + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "OrderDelivered.html";
                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                var messageBody = string.Format(htmlBody, logoPath, orderDTO.OrderNumber.ToString(), homeActionLink);
                await _emailService.SendEmailAsync(orderDTO.User.Email, subject, messageBody, "");  
            }
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
        }
        [HttpGet]
        public async Task<IActionResult> ShipOrder(Guid id, string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusShipped;    
            if (orderDTO.TrackingNumber == null)
            {
                SetorderStatusChangeAction(Result.Fail("Należy nadać numer przesyłki"));
                
            }    
            else
            {
                var result = await _mediator.CommandAsync(command);
                if(result.IsSuccess)
                {
                    var orderDetailsLink = Url.Action("OrdersHistory", "UserSettings", new { area = "Identity" }, Request.Scheme);
                    var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                    var subject = "Potwierdzenie wysłania zamówienia";
                    string htmlBody = "";
                    var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                       + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "OrderSent.html";
                    using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                    {
                        htmlBody = streamReader.ReadToEnd();
                    }
                    var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                    var messageBody = string.Format(htmlBody, logoPath, orderDTO.OrderNumber.ToString(),orderDTO.TrackingNumber, homeActionLink);
                    await _emailService.SendEmailAsync(orderDTO.User.Email, subject, messageBody, "");
                }
                SetorderStatusChangeAction(result);
            }
            return await FetchDetails(id);
        }

        public void SetorderStatusChangeAction(Result result)
        {
            if (result.IsSuccess)
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmieniono status", ActionSuccessfull = true });
            else
            {
                ModelState.Clear();
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = result.Message, ActionSuccessfull = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> StartProccessing(Guid id, string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusInProcess;
            var result = await _mediator.CommandAsync(command);
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> Paid(Guid id, string concurrencyStamp)
        {

            var order = await _mediator.QueryAsync(new GetOrderQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(order);
            command.PaymentStatus = PaymentStatus.PaymentStatusApproved;
            var result = await _mediator.CommandAsync(command);
            if(result.IsSuccess)
            {
                var orderDetailsLink = Url.Action("OrdersHistory", "UserSettings", new { area = "Identity" }, Request.Scheme);
                var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                var subject = "Potwierdzenie zapłaty";
                string htmlBody = "";
                var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                   + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "PaymentConfirmation.html";
                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                var messageBody = string.Format(htmlBody, logoPath, order.OrderNumber.ToString(), orderDetailsLink, homeActionLink);
                await _emailService.SendEmailAsync(order.User.Email, subject, messageBody, "");
            }
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(Guid id, string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusCancelled;
            command.PaymentStatus = PaymentStatus.PaymentStatusCanceled;
            var result = await _mediator.CommandAsync(command);
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
        }


        [HttpGet]
        public async Task<IActionResult> Refunded(Guid id, string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusRefunded;
            command.PaymentStatus = PaymentStatus.PaymentStatusRefunded;
            var result = await _mediator.CommandAsync(command);
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
        }


        [HttpGet]
        public async Task<IActionResult> StartProcessing(Guid id,string concurrencyStamp)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = id });
            var command = _mapper.Map<EditOrderCommand>(orderDTO);
            command.OrderStatus = OrderStatus.OrderStatusInProcess;
            var result = await _mediator.CommandAsync(command);
            SetorderStatusChangeAction(result);
            return await FetchDetails(id);
           
        }




        [HttpPost]
        public async Task<IActionResult> UpdateDetails(OrderDetailsViewModel viewModel)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderQuery() { Id = viewModel.Command.Id });
            var command = new EditOrderCommand();
            _mapper.Map(orderDTO, command);
            command.PostalCode = viewModel.Command.PostalCode;
            command.Street = viewModel.Command.Street;
            command.PhoneNumber = viewModel.Command.PhoneNumber;
            command.City = viewModel.Command.City;
            command.TrackingNumber = viewModel.Command.TrackingNumber;
            command.MessageToUser = viewModel.Command.MessageToUser;
            command.ShippingDate = viewModel.Command.ShippingDate;
            command.DeliveryDate = viewModel.Command.DeliveryDate;
            command.PaymentDate = viewModel.Command.PaymentDate;
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano zamówienie", ActionSuccessfull = true });
                return RedirectToAction("Details", new { id = command.Id });
            }
            var orderReturnDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = command.Id });
            var model = new OrderDetailsViewModel()
            {
                OrderDetails = orderReturnDTO.OrderDetails,
                Command = _mapper.Map<EditOrderCommand>(orderDTO),
                UserEmail = orderReturnDTO.User.Email,
                Carrier = orderReturnDTO.Carrier.Name

            };
            ModelState.PopulateValidation(result.Errors, result.Message, "command.");
            return View("Details", model);
        }
    }
}
