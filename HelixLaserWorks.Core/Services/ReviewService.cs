using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Review;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HelixLaserWorks.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateReviewAsync(ReviewFormModel model, string userId)
        {
            var newReview = new Review()
            {
                Comment = model.Comment,
                Rating = model.Rating,
                CustomerId = userId,
                CreatedOn = DateTime.Now,
            };

            await _context.AddAsync(newReview);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id,
                    Comment = r.Comment ?? string.Empty,
                    Rating = r.Rating,
                    UserEmail = r.Customer.Email,
                    CreatedOn = r.CreatedOn.ToString("MM/dd/yy", CultureInfo.InvariantCulture)
                })
                .ToArrayAsync();
                
        }

        public async Task<bool> UserCanWriteReviewAsync(string userId)
        {
            return await _context.Orders
                .AsNoTracking()
                .AnyAsync(o => o.CustomerId == userId && o.Status == OrderStatus.Completed);
        }

        public async Task<bool> UserHasReviewAsync(string userId)
        {
            return await _context.Reviews.AnyAsync(r => r.CustomerId == userId);
        }
    }
}
