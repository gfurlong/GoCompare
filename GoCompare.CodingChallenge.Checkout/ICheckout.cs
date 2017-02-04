using System;
using System.Collections.Generic;

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
        BasketList Basket { get; }
        decimal TotalPrice();
    }
}
