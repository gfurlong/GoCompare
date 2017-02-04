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
        /// Create a Checkout instance with the default list of available SKUs.
        /// </summary>
        /// <returns></returns>
        ICheckout Create();

        /// <summary>
        /// Create a Checkout instance with a custom list of available SKUs.
        /// </summary>
        /// <param name="skus">A dictionary of available SKU IDs and their associated details</param>
        /// <returns></returns>
        ICheckout Create(SkuDictionary skus);
    }
}
