using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Checkout helper class
    /// </summary>
    /// <remarks>
    /// Instances of this class should only be made using the CheckoutFactory.Create() static method.
    /// Therefore, this class has been marked as "sealed".
    /// </remarks>
    public sealed class Checkout : ICheckout
    {
        private SkuDictionary _skus;
        private BasketList _basket; 

        /// <summary>
        /// The current basket as a dictionary of SKU codes with a running total of items (on offer and not on offer) added per SKU code.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public BasketList Basket
        {
            get
            {
                return _basket;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Checkout() {
            _skus = new SkuDictionary();
            _basket = new BasketList();
        }

        /// <summary>
        /// Secondary constructor for supplying a non-default list of available SKUs
        /// </summary>
        /// <param name="skus"></param>
        public Checkout(SkuDictionary skus) : this()
        {
            _skus = skus;
        }

        /// <summary>
        /// Lookup the details of an item by its SKU ID code.
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns>A formatted string containing the details of the SKU item.</returns>
        public Sku GetSkuDetails(char skuId)
        {
            if (!_skus.ContainsKey(skuId))
                throw new ArgumentException(string.Concat("Missing or invalid SKU ID: ", skuId));

            return _skus[skuId];
        }

        /// <summary>
        /// Add a single item to the basket.
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns>Returns the index of the item added to the basket.</returns>
        public void AddItemToBasket(char skuId)
        {
            if (!_skus.ContainsKey(skuId))
                throw new CheckoutException(string.Concat("Invalid SKU ID: ", skuId));

            // Increment the quantity for the item if it's already in the basket,
            // otherwise add 1 to the basket.
            if(_basket.ContainsKey(skuId)) {
                _basket[skuId]++;
            }
            else 
            {
                _basket.Add(skuId, 1);
            }
        }

        /// <summary>
        /// Add a range of items to the checkout basket.
        /// </summary>
        /// <param name="skuIds">A string of single-character SKU ID codes, e.g. "AAABBD"</param>
        /// <returns>Returns the number of items added to the basket.</returns>
        public int AddRangeOfItemsToBasket(string skuIds)
        {
            foreach (var skuId in skuIds)
            {
                AddItemToBasket(skuId);
            }

            return _basket.Count;
        }

        /// <summary>
        /// Returns the total price of a list of SKUs
        /// </summary>
        /// <returns>The total price of the SKUs added to the checkout basket</returns>
        public decimal TotalPrice()
        {
            var totalPrice = 0.00m;

            foreach (var basketItem in _basket)
            {
                var skuId = basketItem.Key;
                var qty = basketItem.Value;

                var sku = _skus[skuId];

                // If the item has an offer, check if there's enough of the same SKU to qualify...
                if (sku.OfferQty > 0)
                {
                    var qtyApplicableForAnOffer = (int)(qty / sku.OfferQty);
                    var remainders = (qty % sku.OfferQty);

                    totalPrice += (qtyApplicableForAnOffer * sku.OfferPrice);
                    totalPrice += (remainders * sku.IndividualPrice);
                }
                else
                {
                    totalPrice += sku.IndividualPrice;
                }
            }

            return totalPrice;
        }
    }
}