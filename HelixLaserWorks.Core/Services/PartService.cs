﻿using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public async Task<int> CreateAsync(PartFormModel model, string userId, string userEmail)
        {
            Part newPart = new Part()
            {
                Name = model.Name,
                Description = model.Description,
                MaterialId = model.MaterialId,
                Quantity = model.Quantity,
                SchemeURL = await _fileManageService.UploadFile(model.SchemeFile ?? null!, userEmail),
                Thickness = model.PartThickness,
                CreatorId = userId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _context.Parts.AddAsync(newPart);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int partId)
        {
            var parToDelete = await _context.Parts.FindAsync(partId);

            if (parToDelete != null)
            {
                await _fileManageService.DeleteFile(parToDelete.SchemeURL);

                _context.Parts.Remove(parToDelete);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditAsync(PartFormModel model, int partId, string userEmail)
        {
            var parToEdit = await _context.Parts.FindAsync(partId);

            if (parToEdit != null)
            {
                parToEdit.Name = model.Name;
                parToEdit.Description = model.Description;
                parToEdit.MaterialId = model.MaterialId;
                parToEdit.Quantity = model.Quantity;
                parToEdit.Thickness = model.PartThickness;
                parToEdit.UpdatedOn = DateTime.Now;

                if (model.SchemeFile != null && model.SchemeFile.Length > 0)
                {
                    await _fileManageService.DeleteFile(parToEdit.SchemeURL);

                    parToEdit.SchemeURL = await _fileManageService.UploadFile(model.SchemeFile, userEmail);
                }
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
                    PartThickness = p.Thickness,
                })
                .FirstOrDefaultAsync();

            return partForEdit;
        }

        public async Task<UserPartsQueryModel> GetUserPartsAsync(
            string userId,
            int? materialId,
            string? searchTerm,
            PartSorting sorting = PartSorting.Newest,
            int currentPage = 1,
            int partsPerPage = 1)
        {
            var userPartsToShow = _context.Parts.Where(p => p.CreatorId == userId);

            if (materialId != null && materialId != 0)
            {
                userPartsToShow = userPartsToShow
                    .Where(p => p.MaterialId == materialId);
            }

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToUpper();

                userPartsToShow = userPartsToShow.Where(p => p.Name.ToUpper().Contains(normalizedSearchTerm) ||
                                                 p.Description.ToUpper().Contains(normalizedSearchTerm));
            }

            userPartsToShow = sorting switch
            {
                PartSorting.LastUpdated => userPartsToShow.OrderByDescending(p => p.UpdatedOn),

                PartSorting.Quantity => userPartsToShow.OrderByDescending(p => p.Quantity),

                PartSorting.Oldest => userPartsToShow.OrderBy(p => p.CreatedOn),

                _ => userPartsToShow.OrderByDescending(p => p.CreatedOn)
            };

            var parts = await userPartsToShow
                .Skip((currentPage - 1) * partsPerPage)
                .Take(partsPerPage)
                .Select(p => new PartViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Material = p.Material.Name,
                    Quantity = p.Quantity,
                    Thickness = p.Thickness,
                    SchemeFilePath = p.SchemeURL,
                    CreatedOn = p.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    UpdatedOn = p.UpdatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    IsOrdered = p.OrderId != null
                })
                .ToListAsync();

            int totalParts = await userPartsToShow.CountAsync();

            return new UserPartsQueryModel()
            {
                Parts = parts,
                TotalPartsCount = totalParts,
            };
        }

        public async Task<ICollection<PartSelectViewModel>> GetUserPartsForDropdownAsync(string userId)
        {
            return await _context.Parts
                .AsNoTracking()
                .Where(p => p.CreatorId == userId && p.OrderId == null)
                .Select(p => new PartSelectViewModel()
                {
                    Id= p.Id,
                    Name = p.Name,
                    PartMaterial = p.Material.Name,
                    PartThickness = p.Thickness
                })
                .ToListAsync();
        }

        public async Task<bool> IsOrdered(int partId)
        {
            return await _context.Parts.AnyAsync(p => p.Id == partId && p.OrderId != null);
        }

        public async Task<bool> PartExistsAsync(int partId)
        {
            return await _context.Parts.AnyAsync(p => p.Id == partId);
        }

        public async Task<bool> UserIsCreatorAsync(int partId, string currentUserId)
        {
            var part = await _context.Parts.FindAsync(partId);

            if (part != null && part.CreatorId == currentUserId)
            {
                return true;
            }

            return false;
        }
    }
}
