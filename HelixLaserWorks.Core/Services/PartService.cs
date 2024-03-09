using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Parts;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using System.Security.Claims;

namespace HelixLaserWorks.Core.Services
{
    public class PartService : IPartService
    {
        private readonly ApplicationDbContext _context;

        public PartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(PartFormModel model, string userId, string filePath)
        {
            Part newPart = new Part()
            {
                Name = model.Name,
                Description = model.Description,
                MaterialId = model.MaterialId,
                Quantity = model.Quantity,
                SchemeURL = filePath,
                Thickness = model.PartThickness,
                CreatorId = userId
            };

            await _context.Parts.AddAsync(newPart);

            return await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<PartsMineViewModel>> GetUserPartsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
