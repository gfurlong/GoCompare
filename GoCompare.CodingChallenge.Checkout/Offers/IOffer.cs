namespace GoCompare.CodingChallenge.Checkout.Offers
{
    /// <summary>
    /// Public interface used as a mediator to decouple different types of offer used in SKUs.
    /// </summary>
    public interface IOffer
    {
        /// <summary>
        /// Returns the price of a number of items if the offer is valid.
        /// </summary>
        /// <param name="items">The number of items of the offer's SKU code that are in the basket</param>
        /// <param name="nonOfferPrice">The individual (non-offer) price of a SKU item.</param>
        /// <returns></returns>
        decimal OfferPrice(int items, decimal nonOfferPrice);

        /// <summary>
        /// Returns a string representation of the offer.
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
