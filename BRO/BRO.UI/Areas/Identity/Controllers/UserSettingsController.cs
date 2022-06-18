using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.ApplicationUser;
using BRO.Domain.IServices;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.Order;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class UserSettingsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IEmailService _emailService;
        public UserSettingsController(IMediator mediator, IMapper mapper, IEmailService emailService, IWebHostEnvironment hostEnviroment)
        { 
            _mediator = mediator;
            _mapper = mapper;
            _hostEnviroment = hostEnviroment;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> ChangePersonalData()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationUserDTO=await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = Guid.Parse(claim.Value) });
            var model = _mapper.Map<EditApplicationUserCommand>(applicationUserDTO);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePersonalData(EditApplicationUserCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano dane osobowe", ActionSuccessfull = true });
                return RedirectToAction("ChangePersonalData");
            }
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var model = new EditPasswordCommand() { Id= Guid.Parse(claim.Value) };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditPasswordCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if(result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano hasło", ActionSuccessfull = true });
                return RedirectToAction("ChangePassword");
            }
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeConfirmedEmail(Guid userId, string newEmail,string token)
        {
            var result = await _mediator.CommandAsync(new ChangeConfirmedEmailCommand() { Id = userId, NewEmail = newEmail, Token = token });
            if(result.IsSuccess)
            {
                await _mediator.CommandAsync(new LogoutCommand());
                return View("ChangeEmailResult", true);
            }
            return View("ChangeEmailResult", false);
        }
        [HttpGet]
        public async Task<IActionResult> ChangeEmail()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _mediator.QueryAsync(new GetApplicationUserQuery() { Id = Guid.Parse(claim.Value) });

            var model = new ChangeEmailViewModel() { Command= new ChangeEmailCommand() 
            {
                Email = user.Email, Id = user.Id }, 
                IsEmailConfirmed=user.EmailConfirmed 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var email = identity.FindFirst(ClaimTypes.Email);
            var result = await _mediator.CommandAsync(model.Command);
            if(result.IsSuccess)
            {
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie zmodyfikowano email", ActionSuccessfull = true });
                await _mediator.CommandAsync(new LogoutCommand()); 
                await _mediator.CommandAsync(new SignInCommand() { Email = model.Command.Email, RememberMe = true });
                return RedirectToAction("ChangeEmail");     
            } 
            else if (!result.IsSuccess && result.Message == IdentityDetails.EmailChange)
            {
                var token = await _mediator.QueryAsync(new GetEmailChangeTokenQuery() { Id=model.Command.Id, newEmail= model.Command.Email });
                var changeEmailLink = Url.Action("ChangeConfirmedEmail", "UserSettings", new { area = "Identity", userId = model.Command.Id, newEmail = model.Command.Email , token = token}, Request.Scheme);
                var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                var subject = "Zmiana adresu email";
                string htmlBody = "";
                var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
               + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "ChangeEmail.html";
                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                var messageBody = string.Format(htmlBody, logoPath, changeEmailLink, homeActionLink);
                await _emailService.SendEmailAsync(email.Value, subject, messageBody, "");
                TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Potwierdzenie wysłano na adres email", ActionSuccessfull = true });
                return RedirectToAction("ChangeEmail");  
            }
            ModelState.PopulateValidation(result.Errors, result.Message);
            model.Command.PasswordToConfirm =null;
            return View(model);
        }


        public async Task<IActionResult> OrdersHistory()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var orders = await _mediator.QueryAsync(new GetOrdersQuery() { UserId = Guid.Parse(claim.Value)});
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetails(Guid Id)
        {
            var orderDTO = await _mediator.QueryAsync(new GetOrderWithDetailsQuery() { Id = Id });        
            return View(orderDTO);
        }
    }
}
