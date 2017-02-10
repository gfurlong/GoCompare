namespace GoCompare.CodingChallenge.Checkout.Offers
{
    /// <summary>
    /// Implementation of a "x for £y" type of offer, e.g. "3 for £1"
    /// </summary>
    public class QtyBasedOffer : IOffer
    {
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public QtyBasedOffer(int qty, decimal price) {
            Qty = qty;
            Price = price;
        }

        /// <summary>
        /// Get the price if the items qualify for the offer. Otherwise returns the single item price.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="individualPrice"></param>
        /// <returns></returns>
        public decimal OfferPrice(int items, decimal individualPrice)
        {
            var offerPrice = items * individualPrice;

            if (Qty > 0 && items >= Qty)
            {
                var qtyApplicableForAnOffer = (int)(items / Qty);
                var remainders = (items % Qty);

                offerPrice = (qtyApplicableForAnOffer * Price) + (remainders * individualPrice);
            }

            return offerPrice;
        }

        /// <summary>
        /// Returns a string representation of the offer in the format x for £y, e.g. "3 for £1.50"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} for {1:£0.00}", Qty, Price);
        }
    }
}
