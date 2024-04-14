using HelixLaserWorks.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
    public class ReviewController : BaseAdminController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int reviewId)
        {
            if (!await _reviewService.ReviewExistsAsync(reviewId))
            {
                return BadRequest();
            }

            await _reviewService.DeleteReviewAsync(reviewId);

            return RedirectToAction("UsersReviews", "Review", new {area = ""});
        }
    }
}
