using System;
using System.Collections;
using System.Collections.Generic;
using GoCompare.CodingChallenge.Checkout.Offers;

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
        /// Create a Checkout instance with a custom list of available SKUs and offers.
        /// </summary>
        /// <param name="skus">A dictionary of available SKU IDs and their associated details</param>
        /// <param name="offers">A dictionary of offers</param>
        /// <returns></returns>
        public Checkout Create(SkuDictionary skus, OfferDictionary offers)
        {
            return new Checkout(skus, offers);
        }
    }
}