using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string userId, OrderFormModel model);

        Task<ICollection<OrderViewModel>> GetUserOrdersAsync(string userId);

        Task<OrderPaginatedViewModel> GetAllAsync(
            string? userId,
            string? searchTerm = null,
            OrderStatus? status = OrderStatus.Pending,
            int currentPage = 1,
            int partsPerPage = 1);

        Task<bool> OrderExistAsync(int orderId);

        Task<bool> UserIsCreatorAsync(string userId, int orderId);

        Task<OrderStatus> GetOrderStatusAsync(int orderId);

        Task<int> CancelOrderAsync(int orderId);
        
        Task<int> MarkAsReviewdAsync(int orderId);

        Task<OrderDeclineViewModel> GetOrderForDeclineAsync(int orderId);

        Task<OrderViewModel> GetOrderModelForOfferAsync(int orderId);

        Task<int> DeclineOrder(int orderId, OrderDeclineViewModel model);

        Task<bool> HasAnOfferAsync(int orderId);
    }
}
