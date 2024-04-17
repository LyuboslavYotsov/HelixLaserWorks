using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelixLaserWorks.Areas.Admin.Constants.AdminConstants;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class BaseAdminController : Controller
	{
	}
}
