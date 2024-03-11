using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Parts;
using Microsoft.AspNetCore.Mvc;

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

            model.Materials = await _materialService.GetAllForDropdownAsync();
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
                model.Materials = await _materialService.GetAllForDropdownAsync();

                return View(model);
            }
            string userId = GetUserId();
            string userEmail = GetUserEmail();

            if (file != null)
            {
                await _partService.CreateAsync(model, userId, userEmail, file);
            }

            return RedirectToAction(nameof(MyParts));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId();
            var partForEditModel = await _partService.GetPartForEditAsync(id);

            if (partForEditModel == null)
            {
                return BadRequest();
            }

            if (!await _partService.UserIsCreatorAsync(id, userId))
            {
                return Unauthorized();
            }

            partForEditModel.Materials = await _materialService.GetAllForDropdownAsync();

            return View(partForEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PartFormModel model, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                model.Materials = await _materialService.GetAllForDropdownAsync();

                return View(model);
            }

            string userEmail = GetUserEmail();

            await _partService.EditAsync(model, id, userEmail, file);

            return RedirectToAction(nameof(MyParts));
        }

        [HttpPost]
        public async Task<IActionResult> DownloadScheme(string schemeUrl)
        {
            var fileContent = await _fileManageService.DownloadFile(schemeUrl);

            if (fileContent == null)
            {
                return BadRequest();
            }

            string contentType = "application/octet-stream";
            string fileName = Path.GetFileName(schemeUrl);

            return File(fileContent, contentType, fileName);
        }


        [HttpGet]
        public async Task<IActionResult> GetAvailableThicknessesJSON(int id)
        {
            var availableThicknesses = await _materialService.GetAvailableThicknessesForMaterialAsync(id);
            return Json(availableThicknesses);
        }
    }
}
