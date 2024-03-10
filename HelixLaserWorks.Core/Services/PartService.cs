using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Parts;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Core.Services
{
    public class PartService : IPartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManageService _fileManageService;

        public PartService(ApplicationDbContext context,
            IFileManageService fileManageService)
        {
            _context = context;
            _fileManageService = fileManageService;

        }

        public async Task<int> CreateAsync(PartFormModel model, string userId, string userEmail, IFormFile file)
        {
            Part newPart = new Part()
            {
                Name = model.Name,
                Description = model.Description,
                MaterialId = model.MaterialId,
                Quantity = model.Quantity,
                SchemeURL = await _fileManageService.UploadFile(file, userEmail),
                Thickness = model.PartThickness,
                CreatorId = userId
            };

            await _context.Parts.AddAsync(newPart);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditAsync(PartFormModel model, int partId, string userEmail, IFormFile? file)
        {
            var parToEdit = await _context.Parts.FindAsync(partId);

            if (parToEdit == null)
            {
                throw new BadHttpRequestException("Part not found", 400);
            }

            parToEdit.Name = model.Name;
            parToEdit.Description = model.Description;
            parToEdit.MaterialId = model.MaterialId;
            parToEdit.Quantity = model.Quantity;
            parToEdit.Thickness = model.PartThickness;

            if (file != null && file.Length > 0)
            {
                await _fileManageService.DeleteFile(parToEdit.SchemeURL);

                parToEdit.SchemeURL = await _fileManageService.UploadFile(file, userEmail);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<PartFormModel?> GetPartForEditAsync(int id)
        {
            var partForEdit = await _context.Parts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PartFormModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    MaterialId = p.MaterialId,
                    Quantity = p.Quantity,
                    SchemeUrl = p.SchemeURL,
                    PartThickness = p.Thickness
                })
                .FirstOrDefaultAsync();

            return partForEdit;
        }

        public async Task<ICollection<PartViewModel>> GetUserPartsAsync(string userId)
        {
            return await _context.Parts
                .AsNoTracking()
                .Where(p => p.CreatorId == userId)
                .Select(p => new PartViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Material = p.Material.Name,
                    Quantity = p.Quantity,
                    Thickness = p.Thickness,
                    SchemeFilePath = p.SchemeURL
                })
                .ToListAsync();
        }

        public async Task<bool> UserIsCreatorAsync(int id, string currentUserId)
        {
            var part = await _context.Parts.FindAsync(id);

            if (part != null && part.CreatorId == currentUserId)
            {
                return true;
            }

            return false;
        }
    }
}
