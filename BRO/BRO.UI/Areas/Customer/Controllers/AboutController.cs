using BRO.Domain;
using BRO.Domain.Query.Carrier;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AboutController : Controller
    {
        private readonly IMediator _mediator;

        public AboutController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        [HttpGet]
        public async Task<IActionResult> Shipping()
        {
            var model = await _mediator.QueryAsync(new GetCarriersQuery());
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Payments()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PrivacyPolicy()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Refunds()
        {    
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Regulations()
        {
            return View();
        }
    }
}
