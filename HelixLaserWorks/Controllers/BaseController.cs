using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
