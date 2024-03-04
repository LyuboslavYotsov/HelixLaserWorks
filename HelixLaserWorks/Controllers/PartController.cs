using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class PartController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
