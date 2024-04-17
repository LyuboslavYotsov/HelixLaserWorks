using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
	public class Error : BaseController
	{
		[AllowAnonymous]
		public IActionResult InternalServerError()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult PageNotFound()
		{
			return View();
		}
	}
}
