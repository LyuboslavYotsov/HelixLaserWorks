using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Offer;
using HelixLaserWorks.Infrastructure.Data.Models;
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

        [HttpGet]//ADMIN ONLY
        public async Task<IActionResult> Create(int orderId)
        {
            if (!await _orderService.OrderExistAsync(orderId) || await _orderService.HasAnOfferAsync(orderId))
            {
                return BadRequest();
            }

            var model = new OfferFormModel();

            model.Order = await _orderService.GetOrderModelForOfferAsync(orderId);

            model.OrderId = orderId;

            return View(model);
        }

        [HttpPost]//ADMIN ONLY
        public async Task<IActionResult> Create(OfferFormModel model)
        {
            if (!await _orderService.OrderExistAsync(model.OrderId) || await _orderService.HasAnOfferAsync(model.OrderId))
            {
                return BadRequest();
            }

            await _offerService.CreateAsync(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int offerId)
        {
            var userId = GetUserId();

            var model = await _offerService.GetUserOfferDetailsAsync(offerId, userId);

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
    }
}
