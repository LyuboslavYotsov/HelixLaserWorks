using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class OfferController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
