using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

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
        private ICheckoutFactory _factory = new CheckoutFactory();

        [SetUp]
        public void Setup()
        {
            _availableSkus = new SkuDictionary {
                {'A', new Sku("Apple", 50m, 3, 130m)},
                {'B', new Sku("Banana", 30m, 2, 45m)},
                {'C', new Sku("Canteloupe", 20m)},
                {'D', new Sku("Damson", 15m)}
            };
        }

        [Test]
        public void CheckoutTotalPriceOnAddingNewItemsTest()
        {
            var checkout = _factory.Create(_availableSkus);

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
            var checkout = _factory.Create(_availableSkus);

            Assert.Throws<CheckoutException>(() => checkout.AddItemToBasket(' '));
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("A", 50)]            // 1 individual
        [TestCase("AB", 80)]           // 2 individuals
        [TestCase("CDBA", 115)]        // 4 individuals
        [TestCase("AA", 100)]          // 1 offer
        [TestCase("AAA", 130)]         // 1 offer and 1 individual
        [TestCase("AAABB", 175)]       // 2 offers 
        [TestCase("AAABBAA", 275)]     // 3 offers and 1 individual
        [TestCase("AAABBBBBAA", 350)]  // 3 offers and >1 individual
        public void CheckoutTotalPriceCalculationTest(string skuIds, decimal expectedTotalPrice)
        {
            var checkout = _factory.Create(_availableSkus);
            
            checkout.AddRangeOfItemsToBasket(skuIds);

            Assert.That(expectedTotalPrice, Is.EqualTo(checkout.TotalPrice()));
        }

        [Test]
        public void ExceptionThrownIfInvalidSkuSuppliedWhenGettingDetailsTest()
        {
            var checkout = _factory.Create(_availableSkus);

            Assert.Throws<ArgumentException>(() => checkout.GetSkuDetails('~'), "Missing or invalid SKU ID: ~");
        }

        [Test]
        public void ExceptionThrownIfInvalidSkuSuppliedTest()
        {
            const string skuIds = "ABC~~~DEF"; // ( "~" is an invalid SKU code )

            var checkout = _factory.Create(_availableSkus);

            Assert.Throws<CheckoutException>(() => checkout.AddRangeOfItemsToBasket(skuIds), "Invalid SKU ID: ~");
        }
    }
}
