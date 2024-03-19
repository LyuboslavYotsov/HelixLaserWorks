using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string userId, OrderFormModel model);

        Task<ICollection<OrderViewModel>> GetUserOrdersAsync(string userId);

        Task<bool> OrderExistAsync(int orderId);

        Task<bool> UserIsCreatorAsync(string userId, int orderId);

        Task<OrderStatus> GetOrderStatusAsync(int orderId);

        Task<int> CancelOrderAsync(int orderId);
    }
}
