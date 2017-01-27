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
        private Checkout _checkout;

        [OneTimeSetUp]
        public void Setup()
        {
            _checkout = new Checkout(new Dictionary<char, Sku>
                {
                    {'A', new Sku('A', 50, 3, 130)},
                    {'B', new Sku('B', 30, 2, 45)},
                    {'C', new Sku('C', 20)},
                    {'D', new Sku('D', 15)}
                }
            );
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("A", 50)]
        [TestCase("AB", 80)]
        [TestCase("CDBA", 115)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 130)]
        [TestCase("AAABB", 175)]
        [TestCase("AAABBAA", 275)]
        [TestCase("AAABBBBBAA", 350)]
        [TestCase("AAABBBBAA", 320)]
        [TestCase("AAAbbbbAA", 320)]
        public void CheckoutTotalPriceCalculationTest(string skuIds, float expectedTotalPrice)
        {
            Assert.That(expectedTotalPrice, Is.EqualTo(_checkout.TotalPrice(skuIds)));
        }

        [Test]
        public void ExceptionThrownIfInvalidSkuSuppliedTest()
        {
            const string skuIds = "ABC----DEF"; // ( "-" is an invalid SKU code )

            Assert.Throws<CheckoutException>(() => _checkout.TotalPrice(skuIds));
        }
    }
}
