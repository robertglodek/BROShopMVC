using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.ApplicationUser;
using BRO.Domain.Command.ShoppingCart;
using BRO.Domain.IServices;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.StaticDetails;
using BRO.UI.Extensions;
using BRO.UI.Utilities;
using BRO.UI.ViewModels;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Identity.Controllers
{

    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly ICaptchaValidator _captchaValidator;
        public AccountController(IMediator mediator, IMapper mapper, IEmailService emailService, 
            IWebHostEnvironment hostEnviroment, ICaptchaValidator captchaValidator,IConfiguration config)
        {
            _mediator = mediator;
            _mapper = mapper;
            _emailService = emailService;
            _hostEnviroment = hostEnviroment;
            _captchaValidator = captchaValidator;
          
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(loginViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            var result = await _mediator.CommandAsync(model.Command);
            if (result.IsSuccess)
            {
                var sessionShoppingCart = HttpContext.Session.GetObject<List<ShoppingCartItemBasicDTO>>(BRO.UI.Extensions.SessionExtensions.SessionShoppingCart);
                var user = await _mediator.QueryAsync(new GetApplicationUserQuery() { Email = model.Command.Email });
                if(sessionShoppingCart!=null)
                {
                    await _mediator.CommandAsync(new UpdateShoppingCartItemsCommand() { UserId = user.Id, Items = sessionShoppingCart });
                    HttpContext.Session.Remove(BRO.UI.Extensions.SessionExtensions.SessionShoppingCart);
                }
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return LocalRedirect(returnUrl);
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else if (result.Message == IdentityDetails.LoginEmailNotConfirmed)
                return await GenerateEmailConfirmToken(model.Command.Email);
            ModelState.PopulateValidation(result.Errors, result.Message,"");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailWithToken(string email, string token)
        { 
            var result = await _mediator.CommandAsync(new ConfirmEmailCommand() { Email = email, token = token });
            if (result.IsSuccess)
                return View("EmailConfirmationResult", true);
            return View("EmailConfirmationResult", false);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _mediator.CommandAsync(new LogoutCommand());
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AddApplicationUserCommand command,string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.PopulateValidation(new List<Result.Error>(), "Nieprawidłowa weryfikacja captcha");
                return View(command);
            }
            else
            { 
                var result = await _mediator.CommandAsync(command);
                if (result.IsSuccess)
                {
                    var resultLogin = await _mediator.CommandAsync(new LoginApplicationUserCommand() { Email = command.Email, Password = command.Password, RememberMe = false });
                    if (resultLogin.IsSuccess)
                    {
                        TempData.Put("PerformedAction", new PerformedAction() { ActionMessage = "Pomyślnie utworzono konto", ActionSuccessfull = true });
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                        

                    else if (resultLogin.Message == IdentityDetails.LoginEmailNotConfirmed)
                        return await GenerateEmailConfirmToken(command.Email);
                    return RedirectToAction(nameof(Login));
                }
                ModelState.PopulateValidation(result.Errors, result.Message, "");
                return View(command);
            }
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var userEmail = (await _mediator.QueryAsync(new GetApplicationUserQuery() {Id = Guid.Parse(claim.Value)})).Email;
            return await GenerateEmailConfirmToken(userEmail);
        }
        private async Task<IActionResult> GenerateEmailConfirmToken(string email)
        {
            var token = await _mediator.QueryAsync(new GetEmailConfirmTokenQuery() { Email = email });
            var confirmationLink = Url.Action("ConfirmEmailWithToken", "Account", new { area = "Identity", email = email, token = token }, Request.Scheme);
            var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" },Request.Scheme);
            var subject = "Potwierdzenie adresu email";
            string htmlBody = "";
            var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
               + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "ConfirmEmail.html";
            using (StreamReader streamReader= System.IO.File.OpenText(pathToFile))
            {
                htmlBody = streamReader.ReadToEnd();
            }
            var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host)+ "/images/other/logo 170x56.png";
            var messageBody = string.Format(htmlBody,logoPath,confirmationLink, homeActionLink);
            await _emailService.SendEmailAsync(email, subject, messageBody, "");
            return View("EmailConfirmation");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
            {
                var token = await _mediator.QueryAsync(new GetPaswordResetTokenQuery() { Email = command.Email });
                var forgotPasswordLink = Url.Action("ResetPassword", "Account", new { area = "Identity", email = command.Email, token = token} ,Request.Scheme );
                var homeActionLink = Url.Action("Index", "Home", new { area = "Customer" }, Request.Scheme);
                var subject = "Resetowanie hasła";
                string htmlBody = "";
                var pathToFile = _hostEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                   + "templates" + Path.DirectorySeparatorChar.ToString() + "emailTemplates" + Path.DirectorySeparatorChar.ToString() + "PasswordReset.html";
                using (StreamReader streamReader = System.IO.File.OpenText(pathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                var logoPath = string.Format("{0}://{1}", Request.Scheme, Request.Host) + "/images/other/logo 170x56.png";
                var messageBody = string.Format(htmlBody, logoPath, forgotPasswordLink, homeActionLink);
                await _emailService.SendEmailAsync(command.Email, subject, messageBody, "");
                return View("ForgotPasswordConfirmation");
            }
            else if(!result.IsSuccess)
                return View("ForgotPasswordConfirmation");
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            var model = new ResetPasswordCommand() { Email = Email, Token = Token };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _mediator.CommandAsync(command);
            if (result.IsSuccess)
                return View("ResetPasswordResult");
            ModelState.PopulateValidation(result.Errors, result.Message);
            return View(command);
        }
    }
}

