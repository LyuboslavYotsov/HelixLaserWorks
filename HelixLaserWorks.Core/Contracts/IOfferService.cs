using HelixLaserWorks.Core.Models.Offer;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IOfferService
    {
        Task<int> CreateAsync(OfferFormModel model);

        Task<OfferDetailsViewModel?> GetUserOfferDetailsAsync(int offerId, string userId);
        Task<OfferDetailsViewModel?> GetOfferDetailsAsync(int offerId);

        Task<int> AcceptOfferAsync(int offerId);

        Task<bool> OfferExistAsync(int offerId);

        Task<bool> UserIsOrderCreatorAsync(int offerId, string userId);

        Task<bool> OfferIsAcceptedAsync(int offerId);

        Task<ICollection<OfferViewModel>> GetUserOffersAsync(string userId);
    }
}
