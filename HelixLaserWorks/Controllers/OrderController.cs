using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IPartService _partService;

        public OrderController(IOrderService orderService, IPartService partService)
        {
            _orderService = orderService;
            _partService = partService;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            string userId = GetUserId();

            var model = await _orderService.GetUserOrdersAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new OrderFormModel();
            string userId = GetUserId();

            model.UserParts = await _partService.GetUserPartsForDropdownAsync(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderFormModel model)
        {
            string userId = GetUserId();

            foreach (var partId in model.SelectedParts)
            {
                if (!await _partService.UserIsCreatorAsync(partId, userId))
                {
                    ModelState.AddModelError(nameof(model.UserParts), "Cannot add parts created by another user!");
                }
            }

            if (!ModelState.IsValid)
            {
                model.UserParts = await _partService.GetUserPartsForDropdownAsync(userId);
                return View(model);
            }

            await _orderService.CreateOrderAsync(userId, model);

            return RedirectToAction(nameof(MyOrders));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int orderId)
        {
            string userId = GetUserId();

            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            var orderStatus = await _orderService.GetOrderStatusAsync(orderId);

            if (orderStatus != OrderStatus.DeclinedByAdmin &&
                orderStatus != OrderStatus.Pending)
            {
                return BadRequest();
            }

            if (!await _orderService.UserIsCreatorAsync(userId, orderId))
            {
                return Unauthorized();
            }

            await _orderService.CancelOrderAsync(orderId);

            return RedirectToAction(nameof(MyOrders));
        }


        [HttpGet] //ADMIN ONLY
        public async Task<IActionResult> CustomersOrders()
        {
            var model = await _orderService.GetAllOrdersAsync();

            return View(model);
        }

        [HttpPost]//ADMIN ONLY
        public async Task<IActionResult> MarkAsReviewed(int orderId)
        {
            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            await _orderService.MarkAsReviewdAsync(orderId);

            return RedirectToAction(nameof(CustomersOrders));
        }

        [HttpGet]//ADMIN ONLY
        public async Task<IActionResult> Decline(int orderId)
        {
            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            var orderStatus = await _orderService.GetOrderStatusAsync(orderId);

            if (orderStatus == OrderStatus.DeclinedByAdmin ||
                orderStatus == OrderStatus.ReadyWithOffer)
            {
                return BadRequest();
            }

            var model = await _orderService.GetOrderForDeclineAsync(orderId);

            return View(model);
        }

        [HttpPost]//ADMIN ONLY
        public async Task<IActionResult> Decline(int orderId,OrderDeclineViewModel model)
        {
            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            var orderStatus = await _orderService.GetOrderStatusAsync(orderId);

            if (orderStatus == OrderStatus.DeclinedByAdmin ||
                orderStatus == OrderStatus.ReadyWithOffer)
            {
                return BadRequest();
            }

            await _orderService.DeclineOrder(orderId, model);

            return RedirectToAction(nameof(CustomersOrders));
        }
    }
}
