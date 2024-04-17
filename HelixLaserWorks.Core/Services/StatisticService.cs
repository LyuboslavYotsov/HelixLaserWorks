using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Statistics;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext _context;

        public StatisticService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticsViewModel> GetStatisticsAsync()
        {
            int totalOrders = await _context.Orders.CountAsync();
            int totalPendingOrders = await _context.Orders.Where(o => o.Status == OrderStatus.Pending).CountAsync();
            int totalCompletedOrders = await _context.Orders.Where(o => o.Status == OrderStatus.Completed).CountAsync();
            int totalParts = await _context.Parts.CountAsync();
            int totalRegisteredUsers = await _context.Users.CountAsync();
            int totalReviews = await _context.Reviews.CountAsync();

            return new StatisticsViewModel()
            {
                TotalOrders = totalOrders,
                TotalPendingOrders = totalPendingOrders,
                TotalCompletedOrders = totalCompletedOrders,
                TotalPartsCreated = totalParts,
                TotalRegisteredUsers = totalRegisteredUsers,
                TotalReviews = totalReviews,
            };
        }
    }
}
