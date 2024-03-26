using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Offer;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HelixLaserWorks.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;

        public OfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(OfferFormModel model)
        {
            Offer newOffer = new Offer()
            {
                CreatedOn = DateTime.Now,
                DeliveryDueDate = model.DeliveryDueDate,
                Notes = model.Notes,
                Price = model.Price,
                OrderId = model.OrderId
            };

            await _context.Offers.AddAsync(newOffer);
            await _context.SaveChangesAsync();

            var order = await _context.Orders.FindAsync(model.OrderId);

            if (order != null)
            {
                order.Status = OrderStatus.ReadyWithOffer;
                order.OfferId = newOffer.Id;
            }


            return await _context.SaveChangesAsync();
        }

        public async Task<OfferViewModel?> GetUserOfferDetailsAsync(int offerId, string userId)
        {
            return await _context.Offers
                .AsNoTracking()
                .Where(offer => offer.Id == offerId && offer.Order.CustomerId == userId)
                .Select(offer => new OfferViewModel()
                {
                    Id = offer.Id,
                    Price = offer.Price,
                    OrderId = offer.OrderId,
                    AdminNotes = offer.Notes ?? string.Empty,
                    DeliveryDueDate = offer.DeliveryDueDate.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    Order = new OrderViewModel()
                    {
                        Id = offer.Order.Id,
                        Title = offer.Order.Title,
                        CreatedOn = offer.Order.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                        CustomerEmail = offer.Order.Customer.Email,
                        CustomerPhoneNumber = offer.Order.CustomerPhoneNumber,
                        AdminFeedback = offer.Order.AdminFeedback,
                        Description = offer.Order.Description,
                        Status = offer.Order.Status.ToString(),
                        OfferId = offer.Order.OfferId,
                        Parts = offer.Order.Parts.Select(p => new PartSelectViewModel()
                        {
                            Id = p.Id,
                            PartMaterial = p.Material.Name,
                            PartThickness = p.Thickness,
                            Name = p.Name,
                            Quantity = p.Quantity,
                            SchemeUrl = p.SchemeURL
                        })
                        .ToList()
                    }
                })
                .FirstOrDefaultAsync();
        }
    }
}
