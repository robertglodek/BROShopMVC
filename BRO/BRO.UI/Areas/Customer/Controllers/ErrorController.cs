using BRO.Domain;
using BRO.Domain.Utilities.CustomExceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IMediator _mediator;
        public ErrorController(ILogger<ErrorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [Route("Error/{statusCode}")]  
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {    
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if(statusCodeResult!=null)
            {
                _logger.LogWarning($"{statusCode} Erroor Occured. Path={statusCodeResult.OriginalPath} and QueryString ={statusCodeResult.OriginalQueryString}");
                return View("NotFound");
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(context!=null)
            {
                _logger.LogError($"500 Erroor Occured. The path {context.Path} threw and exception " + $"{context.Error}");
                return View("Error");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
