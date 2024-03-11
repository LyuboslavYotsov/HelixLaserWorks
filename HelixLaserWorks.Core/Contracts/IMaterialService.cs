using HelixLaserWorks.Core.Models.Materials;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialViewModel>> AllAsync();

        Task<MaterialDetailViewModel?> GetDetailsByIdAsync(int id);

        Task<ICollection<MaterialDropdownViewModel>> GetAllForDropdownAsync();

        Task<ICollection<double>> GetAvailableThicknessesForMaterialAsync(int id);
    }
}
