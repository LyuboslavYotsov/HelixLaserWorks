using HelixLaserWorks.Core.Models.Order;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string userId, OrderFormModel model);

        Task<ICollection<OrderViewModel>> GetUserOrdersAsync(string userId);
    }
}
