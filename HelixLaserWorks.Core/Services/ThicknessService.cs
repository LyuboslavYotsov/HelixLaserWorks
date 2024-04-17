using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Core.Services
{
    public class ThicknessService : IThicknessService
    {
        private readonly ApplicationDbContext _context;

        public ThicknessService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<double>> GetAllThicknessesAsync()
        {
            return await _context.Thicknesses
                .AsNoTracking()
                .Select(t => t.Value)
                .ToListAsync();
        }

        public async Task<bool> ThicknessesAreValidAsync(ICollection<double> selectedThicknesses)
        {
            var result = true;

            var validThicknesses = await GetAllThicknessesAsync();

            foreach (var thickness in selectedThicknesses)
            {
                if (!validThicknesses.Contains(thickness))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
