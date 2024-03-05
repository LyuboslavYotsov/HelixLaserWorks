using HelixLaserWorks.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await _materialService.All();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _materialService.Details(id);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }
    }
}
