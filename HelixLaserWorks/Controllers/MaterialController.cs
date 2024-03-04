using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class MaterialController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
