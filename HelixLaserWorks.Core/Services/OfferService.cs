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

        public async Task<int> AcceptOfferAsync(int offerId)
        {
            var offerToAccept = await _context.Offers
                .Where(offer => offer.Id == offerId)
                .Include(offer => offer.Order)
                .ThenInclude(order => order.Parts)
                .FirstAsync();

            offerToAccept.IsAccepted = true;

            offerToAccept.Order.Status = OrderStatus.Completed;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(OfferFormModel model)
        {
            Offer newOffer = new Offer()
            {
                CreatedOn = DateTime.Now,
                Notes = model.Notes,
                Price = model.Price,
                OrderId = model.OrderId,
                ProductionDays = model.ProductionDays
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

        public async Task<OfferDetailsViewModel?> GetOfferDetailsAsync(int offerId)
        {
            return await _context.Offers
                .AsNoTracking()
                .Where(offer => offer.Id == offerId)
                .Select(offer => new OfferDetailsViewModel()
                {
                    Id = offer.Id,
                    Price = offer.Price,
                    OrderId = offer.OrderId,
                    AdminNotes = offer.Notes ?? string.Empty,
                    CreatedOn = offer.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    ProductionDays = offer.ProductionDays,
                    IsAccepted = offer.IsAccepted,
                    IsCustomerContacted = offer.IsCustomerContacted,
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

        public async Task<ICollection<OfferViewModel>> GetUserOffersAsync(string userId)
        {
            return await _context.Offers
                .AsNoTracking()
                .Where(offer => offer.Order.CustomerId == userId)
                .OrderByDescending(offer => offer.CreatedOn)
                .Select(offer => new OfferViewModel()
                {
                    Id = offer.Id,
                    AdminNotes = offer.Notes ?? string.Empty,
                    Price = offer.Price,
                    ProductionDays = offer.ProductionDays,
                    OrderName = offer.Order.Title,
                    IsAccepted = offer.IsAccepted,
                    CreatedOn = offer.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    PartsNames = offer.Order.Parts
                        .Select(p => p.Name)
                        .ToList(),
                })
                .ToListAsync();
        }

        public async Task<bool> OfferExistAsync(int offerId)
        {
            return await _context.Offers.AnyAsync(o => o.Id == offerId);
        }

        public async Task<bool> OfferIsAcceptedAsync(int offerId)
        {
            bool result = false;

            var offer = await _context.Offers.FindAsync(offerId);

            if (offer != null && offer.IsAccepted)
            {
                result = true;
            }

            return result;
        }

        public async Task<bool> UserIsOrderCreatorAsync(int offerId, string userId)
        {
            return await _context.Offers
                .AsNoTracking()
                .AnyAsync(offer => offer.Id == offerId && offer.Order.CustomerId == userId);
        }

        public async Task<ICollection<OfferForContactViewModel>> GetOffersWaitingForContactAsync()
        {
            return await _context.Offers
                .AsNoTracking()
                .Where(offer => offer.IsCustomerContacted == false && offer.IsAccepted)
                .OrderByDescending(offer => offer.CreatedOn)
                .Select(offer => new OfferForContactViewModel()
                {
                    Id = offer.Id,
                    AdminNotes = offer.Notes ?? string.Empty,
                    Price = offer.Price,
                    ProductionDays = offer.ProductionDays,
                    OrderName = offer.Order.Title,
                    CreatedOn = offer.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                    CustomerEmail = offer.Order.Customer.Email,
                    CustomerPhone = offer.Order.CustomerPhoneNumber
                })
                .ToListAsync();
        }

        public async Task<int> ContactAchievedAsync(int offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer != null)
            {
                offer.IsCustomerContacted = true;
            }

            return await _context.SaveChangesAsync();
        }
    }
}
