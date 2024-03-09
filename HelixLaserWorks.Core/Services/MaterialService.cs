using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Materials;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixLaserWorks.Core.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationDbContext _context;

        public MaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaterialViewModel>> AllAsync()
        {
            return await _context.Materials
                .AsNoTracking()
                .Select(m => new MaterialViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Type = m.MaterialType.Name,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<MaterialDetailViewModel?> GetDetailsByIdAsync(int id)
        {
            return await _context.Materials
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new MaterialDetailViewModel()
                {
                    Name = m.Name,
                    Description = m.Description,
                    Type = m.MaterialType.Name,
                    ImageUrl = m.ImageUrl,
                    Price = m.PricePerSquareMeter.ToString(),
                    AvailableThicknesses = string.Join(", ", m.MaterialThicknesses.Select(mt => mt.Thickness.Value)),
                    Density = m.Density.ToString(),
                    Rusting = m.CorrosionResistance ? "No" : "Yes"
                })
                .FirstOrDefaultAsync();
        }
    }
}
