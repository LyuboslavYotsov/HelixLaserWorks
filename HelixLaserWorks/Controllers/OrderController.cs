using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
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
        public IActionResult MyOrders()
        {
            return View();
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
        public async Task<IActionResult> Create(OrderFormModel model, List<int> selectedParts)
        {
            model.SelectedParts = selectedParts;

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
    }
}
