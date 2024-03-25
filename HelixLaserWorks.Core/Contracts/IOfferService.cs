﻿using HelixLaserWorks.Core.Models.Offer;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IOfferService
    {
        Task<int> CreateAsync(OfferFormModel model);
    }
}
