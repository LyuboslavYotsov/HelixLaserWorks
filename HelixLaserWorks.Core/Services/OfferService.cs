using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Offer;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.EntityFrameworkCore;

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
    }
}
