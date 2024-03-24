using HelixLaserWorks.Core.Models.Material;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialViewModel>> AllAsync();

        Task<MaterialDetailViewModel?> GetDetailsByIdAsync(int materialId);

        Task<ICollection<MaterialDropdownViewModel>> GetAllForDropdownAsync();

        Task<ICollection<double>> GetAvailableThicknessesForMaterialAsync(int materialId);

        Task<bool> MaterialExistsAsync(int materialId);

        Task<bool> MaterialThicknessExistsAsync(int materialId, double materialThickness);

        Task<int> AddAsync(MaterialFormModel model);

        Task<ICollection<MaterialTypeViewModel>> GetAllATypesAsync();

        Task<bool> MaterialTypeExistsAsync(int typeId);

        Task<MaterialFormModel?> GetMaterialForEditAsync(int materialId);

        Task<int> EditAsync(int materialId, MaterialFormModel model);

        Task<int> DeleteAsync(int materialId);
    }
}
