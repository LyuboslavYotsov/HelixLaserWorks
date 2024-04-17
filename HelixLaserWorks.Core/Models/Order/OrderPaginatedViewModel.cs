using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Material;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Order
{
    public class OrderPaginatedViewModel
    {
        public const int OrdersPerPage = OrdersPerPageDefaultCount;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; } = string.Empty;

        public OrderStatus? Status { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalOrdersCount { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
