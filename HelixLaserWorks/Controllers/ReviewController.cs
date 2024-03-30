using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Review;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> UsersReviews()
        {
            var model = await _reviewService.GetReviewsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string userId = GetUserId();

            if (!await _reviewService.UserCanWriteReviewAsync(userId) || await _reviewService.UserHasReviewAsync(userId))
            {
                return BadRequest();
            }

            var model = new ReviewFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewFormModel model)
        {
            string userId = GetUserId();

            if (!await _reviewService.UserCanWriteReviewAsync(userId) || await _reviewService.UserHasReviewAsync(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _reviewService.CreateReviewAsync(model, userId);

            return RedirectToAction(nameof(UsersReviews));
        }
    }
}
