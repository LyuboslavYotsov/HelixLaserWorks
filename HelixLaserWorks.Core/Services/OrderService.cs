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

                foreach (var part in order.Parts)
                {
                    part.Order = null;
                    part.OrderId = null;
                }

                _context.Orders.Remove(order);
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

        public async Task<int> DeclineOrder(int orderId, OrderDeclineViewModel model)
        {
            var orderToDecline = await _context.Orders.FindAsync(orderId);

            if (orderToDecline != null)
            {
                orderToDecline.Status = OrderStatus.DeclinedByAdmin;

                orderToDecline.AdminFeedback = model.Feedback;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<OrderPaginatedViewModel> GetAllAsync(
            string? searchTerm = null,
            OrderStatus? status = null,
            int currentPage = 1,
            int partsPerPage = 1)
        {
            var ordersToShow = _context.Orders.AsQueryable();

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToUpper();

                ordersToShow = ordersToShow
                    .Where(o => o.Title.ToUpper().Contains(normalizedSearchTerm) ||
                                o.Description.ToUpper().Contains(normalizedSearchTerm));
            }

            if (status != null)
            {
                ordersToShow = status switch
                {
                    OrderStatus.Completed => ordersToShow.Where(o => o.Status == OrderStatus.Completed),
                    OrderStatus.Pending => ordersToShow.Where(o => o.Status == OrderStatus.Pending),
                    OrderStatus.DeclinedByAdmin => ordersToShow.Where(o => o.Status == OrderStatus.DeclinedByAdmin),
                    OrderStatus.InReview => ordersToShow.Where(o => o.Status == OrderStatus.InReview),
                    _ => ordersToShow.Where(o => o.Status == OrderStatus.ReadyWithOffer)
                };
            }

            var orders = await ordersToShow
                .Skip((currentPage - 1) * partsPerPage)
                .Take(partsPerPage)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    AdminFeedback = o.AdminFeedback,
                    Status = o.Status.ToString(),
                    CreatedOn = o.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    OfferId = o.OfferId,
                    CustomerPhoneNumber = o.CustomerPhoneNumber,
                    CustomerEmail = o.Customer.Email,
                    Parts = o.Parts.Select(p => new PartSelectViewModel()
                    {
                        Id = p.Id,
                        PartMaterial = p.Material.Name,
                        PartThickness = p.Thickness,
                        Name = p.Name,
                        Quantity = p.Quantity,
                        SchemeUrl = p.SchemeURL
                    }).ToList()
                })
                .ToListAsync();

            int totalOrdersCount = await ordersToShow.CountAsync();

            return new OrderPaginatedViewModel()
            {
                Orders = orders,
                TotalOrdersCount = totalOrdersCount
            };
        }

        public async Task<OrderDeclineViewModel> GetOrderForDeclineAsync(int orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDeclineViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    UserEmail = o.Customer.Email
                })
                .FirstAsync();
        }

        public async Task<OrderViewModel> GetOrderModelForOfferAsync(int orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    AdminFeedback = o.AdminFeedback,
                    Status = o.Status.ToString(),
                    CreatedOn = o.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    OfferId = o.OfferId,
                    CustomerPhoneNumber = o.CustomerPhoneNumber,
                    CustomerEmail = o.Customer.Email,
                    Parts = o.Parts.Select(p => new PartSelectViewModel()
                    {
                        Id = p.Id,
                        PartMaterial = p.Material.Name,
                        PartThickness = p.Thickness,
                        Name = p.Name,
                        Quantity = p.Quantity,
                        SchemeUrl = p.SchemeURL
                    }).ToList()
                })
                .FirstAsync();
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
                .Where(o => o.CustomerId == userId)
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    AdminFeedback = o.AdminFeedback,
                    Status = o.Status.ToString(),
                    CreatedOn = o.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    OfferId = o.OfferId,
                    CustomerPhoneNumber = o.CustomerPhoneNumber,
                    Parts = o.Parts.Select(p => new PartSelectViewModel()
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

        public async Task<bool> HasAnOfferAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                return order.OfferId != null;
            }

            return false;
        }

        public async Task<int> MarkAsReviewdAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                order.Status = OrderStatus.InReview;
            }

            return await _context.SaveChangesAsync();
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
