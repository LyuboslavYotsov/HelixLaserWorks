using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixLaserWorks.Infrastructure.Data.Models.Enumerators
{
    public enum OrderStatus
    {
        Pending = 0,
        InReview = 1,
        ReadyWithOffer = 2,
        Declined = 3
    }
}
