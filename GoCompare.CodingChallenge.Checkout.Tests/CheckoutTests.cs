using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Microsoft.QualityTools.Testing.Fakes;

using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout.Tests
{
    /// <summary>
    /// Unit tests for the Checkout class members
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CheckoutTests
    {
        private SkuDictionary _availableSkus;
        private OfferDictionary _offers;
        private ICheckoutFactory _factory = new CheckoutFactory();

        [SetUp]
        public void Setup()
        {
            _availableSkus = new SkuDictionary {
                {'A', new Sku("Apple", 50m)},
                {'B', new Sku("Banana", 30m)},
                {'C', new Sku("Canteloupe", 20m)},
                {'D', new Sku("Damson", 15m)}
            };

            _offers = new OfferDictionary {
                {'A', new QtyBasedOffer(3, 130m)},
                {'B', new QtyBasedOffer(2, 45m)}
            };
        }

        [Test]
        public void CheckoutTotalPriceOnAddingNewItemsTest()
        {
            var checkout = _factory.Create(_availableSkus, _offers);

            checkout.AddItemToBasket('A');
            Assert.That(50, Is.EqualTo(checkout.TotalPrice()));
            checkout.AddItemToBasket('B');
            Assert.That(80, Is.EqualTo(checkout.TotalPrice()));
            checkout.AddItemToBasket('C');
            Assert.That(100, Is.EqualTo(checkout.TotalPrice()));
            checkout.AddItemToBasket('D');
            Assert.That(115, Is.EqualTo(checkout.TotalPrice()));

            // Add more items to invoke offers (3 A's for £130), ie. 130 (3 A's) + 30 (B) + 20 (C) + 15 (D) = 195
            checkout.AddItemToBasket('A');
            checkout.AddItemToBasket('A');
            Assert.That(195, Is.EqualTo(checkout.TotalPrice()));

            // Add more items to invoke offers (3 A's for £130), ie. 130 (3 A's) + 50 (A) + 30 (B) + 20 (C) + 15 (D) = 195
            checkout.AddItemToBasket('A');
            Assert.That(245, Is.EqualTo(checkout.TotalPrice()));
        }

        public void ExceptionThrownOnAddingInvalidNewItemTest(char skuId, decimal expectedTotalPrice)
        {
            var checkout = _factory.Create(_availableSkus, _offers);

            Assert.Throws<CheckoutException>(() => checkout.AddItemToBasket(' '));
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("A", 50)]            // 1 individual
        [TestCase("AB", 80)]           // 2 individuals
        [TestCase("CDBA", 115)]        // 4 individuals (no offer)
        [TestCase("AA", 100)]          // 2 individuals @ £50 (no offer)
        [TestCase("AAA", 130)]         // 1 offer
        [TestCase("AAABB", 175)]       // 2 offers 
        [TestCase("AAABBAA", 275)]     // 3 offers and 1 individual
        [TestCase("AAABBBBBAA", 350)]  // 3 offers and >1 individual
        public void CheckoutTotalPriceCalculationTest(string skuIds, decimal expectedTotalPrice)
        {
            var checkout = _factory.Create(_availableSkus, _offers);
            
            checkout.AddRangeOfItemsToBasket(skuIds);

            Assert.That(expectedTotalPrice, Is.EqualTo(checkout.TotalPrice()));
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("AAA", 130)]         // 1 offer
        [TestCase("AAABB", 160)]       // 2 offers, 1 @ 3 for £130, 1 time-based for £15 for each item (£30) = £160 total
        [TestCase("AAABBAA", 260)]     // 2 offers and 2 individuals
        [TestCase("AAABBBBBAA", 305)]  // 3 offers and >1 individual
        public void CheckoutTotalPriceCalculationWithMixedOffersTest(string skuIds, decimal expectedTotalPrice)
        {
            var start = new DateTime(2017, 2, 7);
            var end   = new DateTime(2017, 2, 9);

            var availableSkus = new SkuDictionary {
                {'A', new Sku("Apple", 50m)},
                {'B', new Sku("Banana", 30m)}
            };

            var offers = new OfferDictionary { 
                {'A', new QtyBasedOffer(3, 130m)},
                {'B', new TimeSensitiveOffer(15m, start, end)}            
            };

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return new DateTime(2017, 2, 8); };

                var checkout = _factory.Create(availableSkus, offers);

                checkout.AddRangeOfItemsToBasket(skuIds);

                Assert.That(expectedTotalPrice, Is.EqualTo(checkout.TotalPrice()));
            }
        }


        [Test]
        public void ExceptionThrownIfInvalidSkuSuppliedWhenGettingDetailsTest()
        {
            var checkout = _factory.Create(_availableSkus, _offers);

            Assert.Throws<ArgumentException>(() => checkout.GetSkuDetails('~'), "Missing or invalid SKU ID: ~");
        }

        [Test]
        public void ReturnNullIfInvalidSkuSuppliedWhenGettingOfferDetailsTest()
        {
            var checkout = _factory.Create(_availableSkus, _offers);

            Assert.IsNull(checkout.GetOfferDetails('~'));
        }

        [Test]
        public void ReturnOfferIfValidSkuSuppliedWhenGettingOfferDetailsTest()
        {
            var checkout = _factory.Create(_availableSkus, _offers);

            var offer = (QtyBasedOffer) checkout.GetOfferDetails('A');
            Assert.IsNotNull(offer);
            Assert.AreEqual(3, offer.Qty);
            Assert.AreEqual(130m, offer.Price);
        }

        [Test]
        public void ExceptionThrownIfInvalidSkuSuppliedTest()
        {
            const string skuIds = "ABC~~~DEF"; // ( "~" is an invalid SKU code )

            var checkout = _factory.Create(_availableSkus, _offers);

            Assert.Throws<CheckoutException>(() => checkout.AddRangeOfItemsToBasket(skuIds), "Invalid SKU ID: ~");
        }
    }
}
