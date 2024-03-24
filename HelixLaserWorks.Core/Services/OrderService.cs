using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HelixLaserWorks.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.Parts)
                .FirstOrDefaultAsync();

            if (order != null)
            {
                order.Status = OrderStatus.CanceledByUser;

                foreach (var part in order.Parts)
                {
                    part.Order = null;
                    part.OrderId = null;
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateOrderAsync(string userId, OrderFormModel model)
        {
            Order newOrder = new Order()
            {
                Title = model.Title,
                Description = model.Description,
                Status = OrderStatus.Pending,
                CustomerId = userId,
                CreatedOn = DateTime.Now,
                CustomerPhoneNumber = model.CustomerPhoneNumber
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

        public async Task<OrderStatus> GetOrderStatusAsync(int orderId)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == orderId);

            return order.Status;
        }

        public async Task<ICollection<OrderViewModel>> GetUserOrdersAsync(string userId)
        {
            var userOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == userId && o.Status != OrderStatus.CanceledByUser && o.Status != OrderStatus.DeclinedByAdmin)
                .Select( o => new OrderViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    AdminFeedback = o.AdminFeedback,
                    Status = o.Status.ToString(),
                    CreatedOn = o.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    OfferId = o.OfferId,
                    CustomerPhoneNumber = o.CustomerPhoneNumber,
                    Parts = o.Parts.Select(p => new PartDropdownViewModel()
                    {
                        Id = p.Id,
                        PartMaterial = p.Material.Name,
                        PartThickness = p.Thickness,
                        Name = p.Name,
                        Quantity = p.Quantity,
                    }).ToList()
                })
                .ToListAsync();

            return userOrders;
        }

        public async Task<bool> OrderExistAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderId);
        }

        public async Task<bool> UserIsCreatorAsync(string userId, int orderId)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == orderId);

            return order.CustomerId == userId;
        }
    }
}
