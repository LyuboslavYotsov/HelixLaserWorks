using HelixLaserWorks.Core.Models.Materials;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialViewModel>> AllAsync();

        Task<MaterialDetailViewModel?> GetDetailsByIdAsync(int id);
    }
}
