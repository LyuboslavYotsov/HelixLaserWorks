using HelixLaserWorks.Core.Models.Materials;
using HelixLaserWorks.Core.Models.Parts;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IPartService
    {
        Task<IEnumerable<PartsMineViewModel>> GetUserPartsAsync(string usedId);

        Task<int> CreateAsync(PartFormModel model, string userId, string filePath);
    }
}
