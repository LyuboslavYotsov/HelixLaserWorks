using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class OrderController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
