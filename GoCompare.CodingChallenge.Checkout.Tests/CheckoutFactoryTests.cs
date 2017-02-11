using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout.Tests
{
    /// <summary>
    /// Unit tests for the Checkout class members
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CheckoutFactoryTests
    {
        private ICheckoutFactory _factory;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new CheckoutFactory();
        }

        [Test]
        public void CreateReturnsAnInstanceTest()
        {
            var checkout = _factory.Create(
                new SkuDictionary() {
                    {'A', new Sku("Item A", 10m)},
                    {'B', new Sku("Item B", 2m)}
                },
                new OfferDictionary() {
                    {'A', new QtyBasedOffer(2, 15m)}
                }
            );

            Assert.NotNull(checkout);
            Assert.IsInstanceOf<Checkout>(checkout);
            Assert.That("Item A", Is.EqualTo(checkout.GetSkuDetails('A').Description));
            Assert.That("Item B", Is.EqualTo(checkout.GetSkuDetails('B').Description));
        }
    }
}
