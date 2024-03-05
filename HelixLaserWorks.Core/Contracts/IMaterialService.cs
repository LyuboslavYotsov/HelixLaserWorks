using HelixLaserWorks.Core.Models.Materials;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialViewModel>> All();

        Task<MaterialDetailViewModel?> Details(int id);
    }
}
