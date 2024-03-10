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
    }
}
