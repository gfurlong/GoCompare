using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Checkout helper
    /// </summary>
    public class Checkout : ICheckout
    {
        private IDictionary<char, Sku> _skus;

        /// <summary>
        /// Production constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public Checkout()
        {
            _skus = new Dictionary<char, Sku>
            {
                {'A', new Sku('A', 50, 3, 130)},
                {'B', new Sku('B', 30, 2, 45)},
                {'C', new Sku('C', 20)},
                {'D', new Sku('D', 15)}
            };
        }

        /// <summary>
        /// Protected (unit test) constructor
        /// </summary>
        /// <param name="skus"></param>
        public Checkout(IDictionary<char, Sku> skus)
        {
            _skus = skus;
        }

        /// <summary>
        /// Returns the total price of a list of SKUs
        /// </summary>
        /// <returns>The total price of the SKUs added to the checkout basket</returns>
        public float TotalPrice(string skuIds)
        {
            var totalPrice = 0.0f;
            var itemCounters = new Dictionary<char, int>();

            skuIds = skuIds.ToUpper(); // Ensure all SKU codes are in uppercase.

            try
            {
                foreach (var skuId in skuIds)
                {
                    if (!_skus.ContainsKey(skuId))
                    {
                        throw new CheckoutException(string.Format("Invalid item SKU code \"{0}\" found.", skuId));
                    }

                    // Keep a running total of each item's SKU type so we can apply offers at the end...
                    if (!itemCounters.ContainsKey(skuId))
                    {
                        itemCounters.Add(skuId, 0);
                    }

                    itemCounters[skuId]++;
                }

                // Add up the individual SKUs and add the offer price if there's a multiple...
                foreach (var itemCounter in itemCounters)
                {
                    Console.WriteLine(string.Format("SKU {0}, qty {1}", itemCounter.Key, itemCounter.Value));

                    var sku = _skus[itemCounter.Key];

                    // If the item has an offer, check if there's enough of the same SKU to qualify...
                    if (sku.OfferQty > 0)
                    {
                        var qtyApplicableForAnOffer = (int)(itemCounter.Value / sku.OfferQty);
                        var remainders = (itemCounter.Value % sku.OfferQty);

                        totalPrice += (qtyApplicableForAnOffer * sku.OfferPrice);
                        totalPrice += (remainders * sku.IndividualPrice);
                    }
                    else
                    {
                        totalPrice += sku.IndividualPrice;
                    }
                }
            }
            catch (CheckoutException cex) {
                totalPrice = 0;

                throw cex;
            }

            return totalPrice;
        }
    }
}
