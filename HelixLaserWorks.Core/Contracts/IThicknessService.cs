namespace HelixLaserWorks.Core.Contracts
{
    public interface IThicknessService
    {
        Task<ICollection<double>> GetAllThicknessesAsync();

        Task<bool> ThicknessesAreValidAsync(ICollection<double> selectedThicknesses);
    }
}
