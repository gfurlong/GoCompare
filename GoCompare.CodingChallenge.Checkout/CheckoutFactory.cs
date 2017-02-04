using System;
using System.Collections;
using System.Collections.Generic;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Checkout factory for creating a new Checkout instance
    /// </summary>
    /// <remarks>
    /// By using the factory pattern, a calling application can create multiple instances of
    /// the Checkout class. E.g. in a web application that has multiple user sessions, each with their
    /// own Checkout instance.
    /// </remarks>
    public class CheckoutFactory : ICheckoutFactory
    {
        /// <summary>
        /// Create a Checkout instance with the default list of available SKUs.
        /// </summary>
        /// <returns></returns>
        public ICheckout Create()
        {
            return new Checkout(new SkuDictionary {
                {'A', new Sku("Apple", 50m, 3, 130m)},
                {'B', new Sku("Banana", 30m, 2, 45m)},
                {'C', new Sku("Canteloupe", 20m)},
                {'D', new Sku("Damson", 15m)}
            });
        }

        /// <summary>
        /// Create a Checkout instance with a custom list of available SKUs.
        /// </summary>
        /// <param name="skus">A dictionary of available SKU IDs and their associated details</param>
        /// <returns></returns>
        public ICheckout Create(SkuDictionary skus)
        {
            return new Checkout(skus);
        }
    }
}