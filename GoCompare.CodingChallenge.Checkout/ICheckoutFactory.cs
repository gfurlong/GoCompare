using System;
using System.Collections.Generic;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Checkout factory interface
    /// </summary>
    public interface ICheckoutFactory
    {
        /// <summary>
        /// Create a Checkout instance with a custom list of available SKUs.
        /// </summary>
        /// <param name="skus">A dictionary of available SKU IDs and their associated details</param>
        /// <param name="offers">A dictionary of offers</param>
        /// <returns></returns>
        Checkout Create(SkuDictionary skus, OfferDictionary offers);
    }
}
