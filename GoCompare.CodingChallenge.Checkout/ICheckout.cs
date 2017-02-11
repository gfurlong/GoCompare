using System;
using System.Collections.Generic;

using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Public interface for a checkout
    /// </summary>
    public interface ICheckout
    {
        void AddItemToBasket(char skuId);
        int AddRangeOfItemsToBasket(string skuIds);
        Sku GetSkuDetails(char skuId);
        BasketDictionary Basket { get; }
        decimal TotalPrice();

        IOffer GetOfferDetails(char skuId);
    }
}
