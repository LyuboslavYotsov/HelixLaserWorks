using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
