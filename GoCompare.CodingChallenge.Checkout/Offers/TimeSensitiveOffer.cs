using System;

namespace GoCompare.CodingChallenge.Checkout.Offers
{
    /// <summary>
    /// Implementation of a "x for £y" type of offer, e.g. "3 for £1"
    /// </summary>
    public class TimeSensitiveOffer : IOffer
    {
        public decimal  Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End   { get; set; }

        public TimeSensitiveOffer(decimal price, DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("The start time of a time-sensitive offer must be before it's end time.");

            Price = price;
            Start = start;
            End = end;
        }

        /// <summary>
        /// Get the price if the items qualify for the offer. Otherwise returns the single item price.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="individualPrice"></param>
        /// <returns></returns>
        public decimal OfferPrice(int items, decimal individualPrice)
        {
            var today = DateTime.Now;
            var price = ((today >= Start && today <= End)
                                ? Price
                                : individualPrice);

            return items * price;
        }

        /// <summary>
        /// Returns a string representation of the offer in the format x for £y, e.g. "3 for £1.50"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0:£0.00} if bought between {1:dd MMM yyyy} and {2:dd MMM yyyy}", Price, Start, End);
        }
    }
}
