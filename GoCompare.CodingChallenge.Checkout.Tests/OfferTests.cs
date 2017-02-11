using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System;
using Microsoft.QualityTools.Testing.Fakes;
using NUnit.Framework;

using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout.Tests
{
    /// <summary>
    /// Unit tests for classes implementing IOffer
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class OfferTests
    {
        [Test]
        public void OfferPriceForQtyBasedOfferWhenQtyIsZeroTest()
        {
            const decimal price = 20m;
            var offer = new QtyBasedOffer(0, 15m);

            var offerPrice = offer.OfferPrice(1, price);

            Assert.NotNull(offer);

            Assert.That(price, Is.EqualTo(offerPrice));
        }

        [Test]
        public void OfferPriceForQtyBasedOfferTest()
        {
            const int qty = 3;
            const decimal individualPrice = 130m;
            const decimal offerPrice      = 100m;

            var offer = new QtyBasedOffer(qty, offerPrice);

            var price = offer.OfferPrice(qty, individualPrice);

            Assert.NotNull(offer);
            
            Assert.That(offerPrice, Is.EqualTo(price));
        }

        [Test]
        public void ToStringForQtyBasedOfferTest()
        {
            const int qty = 3;
            const decimal offerPrice = 100m;

            var offer = new QtyBasedOffer(qty, offerPrice);

            Assert.NotNull(offer);
            Assert.AreEqual("3 for £100.00", offer.ToString());
        }

        [Test]
        public void ThrowExceptionIfTimeBasedOfferHasInvalidDatesTest()
        {
            const decimal offerPrice = 100m;

            var today = new DateTime(2017, 2, 10);
            var start = today.AddDays(1);
            var end = today.AddDays(-2);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return today; };

                Assert.Throws<ArgumentException>(() => new TimeSensitiveOffer(offerPrice, start, end));
            }
        }

        [Test]
        public void OfferPriceForInvalidTimeBasedOfferTest()
        {
            const int qty = 1;
            const decimal individualPrice = 130m;
            const decimal offerPrice = 100m;

            var today = new DateTime(2017, 2, 10);
            var start = today.AddDays(1);
            var end = today.AddDays(2);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return today; };
                var offer = new TimeSensitiveOffer(offerPrice, start, end);

                var price = offer.OfferPrice(qty, individualPrice);

                Assert.NotNull(offer);
                Assert.That(individualPrice, Is.EqualTo(price));
            }
        }

        [Test]
        public void OfferPriceForTimeBasedOfferTest()
        {
            const int qty = 1;
            const decimal individualPrice = 130m;
            const decimal offerPrice = 100m;

            var today = new DateTime(2017, 2, 10);
            var start = today.AddDays(-1);
            var end = today.AddDays(1);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return today; };
                var offer = new TimeSensitiveOffer(offerPrice, start, end);

                var price = offer.OfferPrice(qty, individualPrice);

                Assert.NotNull(offer);
                Assert.That(offerPrice, Is.EqualTo(price));
            }
        }

        [Test]
        public void ToStringForTimeBasedOfferTest()
        {
            const decimal offerPrice = 100m;
            var today = new DateTime(2017, 2, 10);
            var start = today.AddDays(-1);
            var end = today.AddDays(1);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return today; };

                var offer = new TimeSensitiveOffer(offerPrice, start, end);

                Assert.NotNull(offer);
                Assert.AreEqual(string.Format("£{0:0.00} from {1:dd MMM yyyy} to {2:dd MMM yyyy}",  offerPrice, start, end),
                    offer.ToString());
            }
        }    
    }
}
