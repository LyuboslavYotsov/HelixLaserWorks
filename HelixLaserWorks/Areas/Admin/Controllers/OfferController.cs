using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Offer;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
    public class OfferController : BaseAdminController
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
        public async Task<IActionResult> Create(int orderId, OfferFormModel model)
        {
            if (!await _orderService.OrderExistAsync(model.OrderId) || await _orderService.HasAnOfferAsync(model.OrderId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.Order = await _orderService.GetOrderModelForOfferAsync(orderId);
                return View(model);
            }

            await _offerService.CreateAsync(model);

            return RedirectToAction("CustomersOrders", "Order");
        }

        [HttpGet] //ADMIN ONLY
        public async Task<IActionResult> Details(int offerId)
        {
            var model = await _offerService.GetOfferDetailsAsync(offerId);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]// ADMIN ONLY
        public async Task<IActionResult> ForContact()
        {
            var model = await _offerService.GetOffersWaitingForContactAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Contacted(int offerId)
        {

            if (!await _offerService.OfferExistAsync(offerId))
            {
                return BadRequest();
            }

            await _offerService.ContactAchievedAsync(offerId);

            return RedirectToAction(nameof(ForContact));
        }
    }
}
