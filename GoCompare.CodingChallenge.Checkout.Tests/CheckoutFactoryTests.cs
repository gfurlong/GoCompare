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
    public class CheckoutFactoryTests
    {
        private ICheckoutFactory _factory;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new CheckoutFactory();
        }

        [Test]
        public void DefaultCreateReturnsAnInstanceTest()
        {
            var checkout = _factory.Create();

            Assert.NotNull(checkout);
            Assert.IsInstanceOf<Checkout>(checkout);
        }

        [Test]
        public void CreateReturnsAnInstanceTest()
        {
            var checkout = _factory.Create(new SkuDictionary() {
                { 'A', new Sku("Item A", 10m, 2, 15m)},
                { 'B', new Sku("Item B", 2m)}
            } );

            Assert.NotNull(checkout);
            Assert.IsInstanceOf<Checkout>(checkout);
            Assert.That("Item A", Is.EqualTo(checkout.GetSkuDetails('A').Description));
            Assert.That("Item B", Is.EqualTo(checkout.GetSkuDetails('B').Description));
        }
    }
}
