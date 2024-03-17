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

        public async Task<ICollection<OrderViewModel>> GetUserOrdersAsync(string userId)
        {
            var userOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == userId)
                .Select( o => new OrderViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    AdminFeedback = o.AdminFeedback,
                    Status = o.Status.ToString(),
                    CreatedOn = o.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    OfferId = o.OfferId,
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
    }
}
