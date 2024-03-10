using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Parts;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class PartController : BaseController
    {
        private readonly IPartService _partService;
        private readonly IMaterialService _materialService;

        public PartController(IPartService partService,
            IMaterialService materialService)
        {
            _materialService = materialService;
            _partService = partService;
        }

        public IActionResult Mine()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PartFormModel();

            model.Materials = await _materialService.GetAllForDropdownAsyc();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartFormModel model, IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError("File Error", "Invalid file, try uploading it again!");
            }

            if (!ModelState.IsValid)
            {
                model.Materials = await _materialService.GetAllForDropdownAsyc();

                return View(model);
            }
            string userId = GetUserId();

            await _partService.CreateAsync(model, userId, file);

            return RedirectToAction(nameof(Mine));
        }
    }
}
