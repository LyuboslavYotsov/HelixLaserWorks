﻿using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Part;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IPartService
    {
        Task<UserPartsQueryModel> GetUserPartsAsync(
             string userId,
            int? materialId = null,
            string? searchTerm = null,
            PartSorting sorting = PartSorting.Newest,
            int currentPage = 1,
            int partsPerPage = 1);

        Task<int> CreateAsync(PartFormModel model, string userId, string userEmail);

        Task<PartFormModel?> GetPartForEditAsync(int id);

        Task<int> EditAsync(PartFormModel model, int partId, string userEmail);

        Task<bool> UserIsCreatorAsync(int partId, string currentUserId);

        Task<bool> PartExistsAsync(int partId);

        Task<bool> IsOrdered(int partId);

        Task<int> DeleteAsync(int partId);

        Task<ICollection<PartSelectViewModel>> GetUserPartsForDropdownAsync(string userId);
    }
}
