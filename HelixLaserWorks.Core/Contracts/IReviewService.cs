using HelixLaserWorks.Core.Models.Review;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> UserCanWriteReviewAsync(string userId);

        Task<bool> UserHasReviewAsync(string userId);

        Task<IEnumerable<ReviewViewModel>> GetReviewsAsync();

        Task<int> CreateReviewAsync(ReviewFormModel model, string userId);
    }
}
