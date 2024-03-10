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

        [HttpGet]
        public async Task<IActionResult> MyParts()
        {
            string userId = GetUserId();

            var userParts = await _partService.GetUserPartsAsync(userId);

            return View(userParts);
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
            string userEmail = GetUserEmail();

            await _partService.CreateAsync(model, userId, userEmail, file);

            return RedirectToAction(nameof(MyParts));
        }
    }
}
