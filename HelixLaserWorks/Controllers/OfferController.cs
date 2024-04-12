using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Offer;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class OfferController : BaseController
    {
        private readonly IOfferService _offerService;
        private readonly IOrderService _orderService;

        public OfferController(IOfferService offerService,
            IOrderService orderService)
        {
            _offerService = offerService;
            _orderService = orderService;

        }

        [HttpGet]
        public async Task<IActionResult> Details(int offerId)
        {
            var model = await _offerService.GetOfferDetailsAsync(offerId);

            if (model == null)
            {
                return BadRequest();
            }

            if (model.Order.CustomerEmail != GetUserEmail())
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyOffers()
        {
            var userId = GetUserId();

            var model = await _offerService.GetUserOffersAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int offerId)
        {
            if (!await _offerService.OfferExistAsync(offerId) || await _offerService.OfferIsAcceptedAsync(offerId))
            {
                return BadRequest();
            }

            var userId = GetUserId();

            if (!await _offerService.UserIsOrderCreatorAsync(offerId, userId))
            {
                return Unauthorized();
            }

            await _offerService.AcceptOfferAsync(offerId);

            return RedirectToAction(nameof(OfferAccepted));
        }

        [HttpGet]
        public IActionResult OfferAccepted()
        {
            return View();
        }

    }
}
