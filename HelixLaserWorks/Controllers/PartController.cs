using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Parts;
using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelixLaserWorks.Controllers
{
    public class PartController : BaseController
    {
        private readonly IPartService _partService;
        private readonly IMaterialService _materialService;
        private readonly IFileManageService _fileManageService;

        public PartController(IPartService partService,
            IMaterialService materialService,
            IFileManageService fileManageService)
        {
            _materialService = materialService;
            _partService = partService;
            _fileManageService = fileManageService;

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
                ModelState.AddModelError("File Error", "Invalid file, try again!");

            }

            if (!ModelState.IsValid)
            {
                model.Materials = await _materialService.GetAllForDropdownAsyc();

                return View(model);
            }

            string filePath = await _fileManageService.UploadFile(file);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _partService.CreateAsync(model, userId, filePath);

            return RedirectToAction(nameof(Mine));
        }
    }
}
