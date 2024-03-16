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
        public async Task<IActionResult> MyParts([FromQuery] UserPartsQueryModel model)
        {
            string userId = GetUserId();

            var userParts = await _partService.GetUserPartsAsync(userId,
                model.Material,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                UserPartsQueryModel.PartsPerPage
                );

            model.TotalPartsCount = userParts.TotalPartsCount;
            model.Parts = userParts.Parts;

            model.Materials = await _materialService.GetAllForDropdownAsync();

            return View(model);
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
                ModelState.AddModelError("FileError", "Invalid file, try uploading it again!");
            }

            if (!await _materialService.MaterialExistsAsync(model.MaterialId))
            {
                ModelState.AddModelError(nameof(model.MaterialId), "Invalid material!");
            }

            if (!await _materialService.MaterialThicknessExistsAsync(model.MaterialId, model.PartThickness))
            {
                ModelState.AddModelError(nameof(model.PartThickness), "Invalid thickness for given material!");
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
        public async Task<IActionResult> Edit(int partId)
        {
            string userId = GetUserId();
            var partForEditModel = await _partService.GetPartForEditAsync(partId);

            if (partForEditModel == null)
            {
                return BadRequest();
            }

            if (!await _partService.UserIsCreatorAsync(partId, userId))
            {
                return Unauthorized();
            }

            partForEditModel.Materials = await _materialService.GetAllForDropdownAsync();

            return View(partForEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int partId, PartFormModel model, IFormFile? file)
        {
            string userId = GetUserId();

            if (!await _partService.PartExistsAsync(partId))
            {
                return BadRequest();
            }

            if (!await _partService.UserIsCreatorAsync(partId, userId))
            {
                return Unauthorized();
            }

            if (!await _materialService.MaterialExistsAsync(model.MaterialId))
            {
                ModelState.AddModelError(nameof(model.MaterialId), "Invalid material!");
            }

            if (!await _materialService.MaterialThicknessExistsAsync(model.MaterialId, model.PartThickness))
            {
                ModelState.AddModelError(nameof(model.PartThickness), "Invalid thickness for given material!");
            }

            if (!ModelState.IsValid)
            {
                model.Materials = await _materialService.GetAllForDropdownAsync();

                return View(model);
            }

            string userEmail = GetUserEmail();

            await _partService.EditAsync(model, partId, userEmail, file);

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


        [HttpPost]
        public async Task<IActionResult> Delete(int partId)
        {
            if (!await _partService.PartExistsAsync(partId))
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!await _partService.UserIsCreatorAsync(partId, userId))
            {
                return Unauthorized();
            }

            await _partService.DeleteAsync(partId);

            return RedirectToAction(nameof(MyParts));
        }
    }
}
