using HelixLaserWorks.Core.Models.Materials;
using HelixLaserWorks.Core.Models.Parts;
using Microsoft.AspNetCore.Http;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IPartService
    {
        Task<ICollection<PartViewModel>> GetUserPartsAsync(string userId);

        Task<int> CreateAsync(PartFormModel model, string userId, string userEmail, IFormFile file);
    }
}
