using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
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

        public async Task<int> AddAsync(MaterialFormModel model)
        {
            Material newMaterial = new Material()
            {
                MaterialTypeId = model.MaterialTypeId,
                Name = model.Name,
                Description = model.Description,
                Specification = model.Specification,
                CorrosionResistance = model.CorrosionResistance,
                Density = model.Density,
                PricePerSquareMeter = model.PricePerSquareMeter,
                ImageUrl = model.ImageUrl,
            };

            foreach (var thickness in model.SelectedThicknesses)
            {
                var currentThickness = await _context.Thicknesses.FirstOrDefaultAsync(t => t.Value == thickness);

                if (currentThickness != null)
                {
                    newMaterial.MaterialThicknesses.Add(new MaterialThickness()
                    {
                        ThicknessId = currentThickness.Id,
                        Material = newMaterial
                    });
                }
            }

            await _context.Materials.AddAsync(newMaterial);

            return await _context.SaveChangesAsync();
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

        public async Task<int> DeleteAsync(int materialId)
        {
            var materialToDelete = await _context.Materials.FindAsync(materialId);

            if (materialToDelete != null)
            {
                var materialThicknesses = await _context.MaterialsThicknesses
                    .Where(mt => mt.MaterialId == materialToDelete.Id)
                    .ToListAsync();

                _context.MaterialsThicknesses.RemoveRange(materialThicknesses);

                _context.Materials.Remove(materialToDelete);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditAsync(int materialId, MaterialFormModel model)
        {
            Material? materialToEdit = await _context.Materials
                .Include(m => m.MaterialThicknesses)
                .ThenInclude(mt => mt.Thickness)
                .Where(m => m.Id == materialId)
                .FirstOrDefaultAsync();

            if (materialToEdit != null)
            {
                materialToEdit.Name = model.Name;
                materialToEdit.Description = model.Description;
                materialToEdit.Specification = model.Specification;
                materialToEdit.Density = model.Density;
                materialToEdit.PricePerSquareMeter = model.PricePerSquareMeter;
                materialToEdit.ImageUrl = model.ImageUrl;
                materialToEdit.CorrosionResistance = model.CorrosionResistance;
                materialToEdit.MaterialTypeId = model.MaterialTypeId;

                _context.MaterialsThicknesses.RemoveRange(materialToEdit.MaterialThicknesses);

                foreach (var selectedThicknessValue in model.SelectedThicknesses)
                {
                    var currentThickness = await _context.Thicknesses.FirstOrDefaultAsync(t => t.Value == selectedThicknessValue);

                    if (currentThickness != null)
                    {
                        materialToEdit.MaterialThicknesses.Add(new MaterialThickness()
                        {
                            ThicknessId = currentThickness.Id,
                            Material = materialToEdit
                        });
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<ICollection<MaterialTypeViewModel>> GetAllATypesAsync()
        {
            return await _context.MaterialTypes
                .AsNoTracking()
                .Select(m => new MaterialTypeViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                })
                .ToListAsync();
        }

        public async Task<ICollection<MaterialDropdownViewModel>> GetAllForDropdownAsync()
        {
            return await _context.Materials
                .AsNoTracking()
                .Select(m => new MaterialDropdownViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                })
                .ToListAsync();
        }

        public async Task<ICollection<double>> GetAvailableThicknessesForMaterialAsync(int materialId)
        {
            return await _context.MaterialsThicknesses
                .AsNoTracking()
                .Where(mt => mt.MaterialId == materialId)
                .Select(mt => mt.Thickness.Value)
                .ToListAsync();
        }

        public async Task<MaterialDetailViewModel?> GetDetailsByIdAsync(int materialId)
        {
            return await _context.Materials
                .AsNoTracking()
                .Where(m => m.Id == materialId)
                .Select(m => new MaterialDetailViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Type = m.MaterialType.Name,
                    ImageUrl = m.ImageUrl,
                    Price = m.PricePerSquareMeter.ToString(),
                    AvailableThicknesses = string.Join(", ", m.MaterialThicknesses.Select(mt => mt.Thickness.Value + "mm")),
                    Density = m.Density.ToString(),
                    Rusting = m.CorrosionResistance ? "No" : "Yes"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MaterialFormModel?> GetMaterialForEditAsync(int materialId)
        {
            return await _context.Materials
                .AsNoTracking()
                .Where(m => m.Id == materialId)
                .Select(m => new MaterialFormModel()
                {
                    Name = m.Name,
                    Description = m.Description,
                    Density = m.Density,
                    Specification = m.Specification,
                    SelectedThicknesses = m.MaterialThicknesses.Select(mt => mt.Thickness.Value).ToList(),
                    ImageUrl = m.ImageUrl,
                    PricePerSquareMeter = m.PricePerSquareMeter,
                    CorrosionResistance = m.CorrosionResistance,
                    MaterialTypeId = m.MaterialTypeId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MaterialExistsAsync(int materialId)
        {
            return await _context.Materials.AnyAsync(m => m.Id == materialId);
        }

        public async Task<bool> MaterialThicknessExistsAsync(int materialId, double materialThickness)
        {
            var material = await _context.Materials.Where(m => m.Id == materialId)
                .Include(m => m.MaterialThicknesses)
                .ThenInclude(mt => mt.Thickness)
                .FirstAsync();

            if (material.MaterialThicknesses.Any(mt => mt.Thickness.Value == materialThickness))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> MaterialTypeExistsAsync(int typeId)
        {
            return await _context.MaterialTypes.AnyAsync(m => m.Id == typeId);
        }
    }
}
