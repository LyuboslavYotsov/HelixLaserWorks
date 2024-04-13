using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService _orderService;
        private readonly IPartService _partService;

        public OrderController(IOrderService orderService, IPartService partService)
        {
            _orderService = orderService;
            _partService = partService;
        }


        [HttpGet]//ADMIN ONLY
        public async Task<IActionResult> CustomersOrders([FromQuery] OrderPaginatedViewModel model)
        {
            var orders = await _orderService.GetAllAsync(
                null,
                model.SearchTerm,
                model.Status,
                model.CurrentPage,
                OrderPaginatedViewModel.OrdersPerPage);

            model.TotalOrdersCount = orders.TotalOrdersCount;
            model.Orders = orders.Orders;

            return View(model);
        }

        [HttpPost]//ADMIN ONLY
        public async Task<IActionResult> MarkAsReviewed(int orderId)
        {
            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            var orderStatus = await _orderService.GetOrderStatusAsync(orderId);

            if (orderStatus != OrderStatus.Pending)
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
        public async Task<IActionResult> Decline(int orderId, OrderDeclineViewModel model)
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

        [HttpPost]//ADMIN ONLY
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (!await _orderService.OrderExistAsync(orderId))
            {
                return BadRequest();
            }

            await _orderService.DeleteOrderAsync(orderId);

            return RedirectToAction(nameof(CustomersOrders));
        }
    }
}
