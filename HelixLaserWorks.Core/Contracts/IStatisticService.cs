using HelixLaserWorks.Core.Models.Statistics;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IStatisticService
    {
        Task<StatisticsViewModel> GetStatisticsAsync();
    }
}
