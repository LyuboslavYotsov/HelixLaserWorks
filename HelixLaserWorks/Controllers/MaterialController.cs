using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
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
            var models = await _materialService.AllAvailableAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int materialid)
        {
            var model = await _materialService.GetDetailsByIdAsync(materialid);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableThicknessesJSON(int materialId)
        {
            var availableThicknesses = await _materialService.GetAvailableThicknessesForMaterialAsync(materialId);
            return Json(availableThicknesses);
        }
    }
}
