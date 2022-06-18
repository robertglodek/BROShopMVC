using BRO.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BRO.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TutorialController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Article(string name)
        {
           if(StaticDetails.GetAvailableTutorialsNames().Contains(name))
                return View(name);
            return NotFound();
        }
    }
}
