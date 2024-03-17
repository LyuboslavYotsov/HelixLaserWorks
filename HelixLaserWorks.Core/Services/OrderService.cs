using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;

namespace HelixLaserWorks.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(string userId, OrderFormModel model)
        {
            Order newOrder = new Order()
            {
                Title = model.Title,
                Description = model.Description,
                Status = OrderStatus.Pending,
                CustomerId = userId,
                CreatedOn = DateTime.Now
            };

            foreach (var partId in model.SelectedParts)
            {
                var part = await _context.Parts.FindAsync(partId);

                if (part != null)
                {
                    newOrder.Parts.Add(part);
                }
            }

            await _context.Orders.AddAsync(newOrder);

            return await _context.SaveChangesAsync();
        }
    }
}
