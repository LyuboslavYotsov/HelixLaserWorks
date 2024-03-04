using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class ReviewController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
