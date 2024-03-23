using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;
        private readonly IThicknessService _thicknessService;

        public MaterialController(IMaterialService materialService,
            IThicknessService thicknessService)
        {
            _materialService = materialService;
            _thicknessService = thicknessService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await _materialService.AllAsync();

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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new MaterialFormModel();

            model.AvailableThicknesses = await _thicknessService.GetAllThicknessesAsync();

            model.MaterialTypes = await _materialService.GetAllATypesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MaterialFormModel model)
        {
            if (!await _materialService.MaterialTypeExistsAsync(model.MaterialTypeId))
            {
                ModelState.AddModelError(nameof(model.MaterialTypeId), "Material type does not exists!");
            }

            var availableThicknesses = await _thicknessService.GetAllThicknessesAsync();

            foreach (var thickness in model.SelectedThicknesses)
            {
                if (!availableThicknesses.Contains(thickness))
                {
                    ModelState.AddModelError(nameof(model.SelectedThicknesses), "Invalid thickness!");
                    break;
                }
            }

            if (!ModelState.IsValid)
            {
                model.AvailableThicknesses = await _thicknessService.GetAllThicknessesAsync();

                model.MaterialTypes = await _materialService.GetAllATypesAsync();

                return View(model);
            }

            await _materialService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
