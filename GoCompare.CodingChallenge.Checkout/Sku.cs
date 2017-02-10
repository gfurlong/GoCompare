using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// Stock Keeping Unit (SKU)
    /// </summary>
    public struct Sku
    {
        /// <summary>
        /// Create a new SKU object
        /// </summary>
        /// <param name="skuId">SKU ID code</param>
        /// <param name="description">Description of the SKU</param>
        /// <param name="individualPrice">Pricate of the SKU if not part of an offer</param>
        /// <param name="offer">If the SKU has an offer, set this parameter, otherwise leave it null</param>
        public Sku(string description, decimal individualPrice, IOffer offer = null)
        {
            Description = description;
            IndividualPrice = individualPrice;

            Offer = offer;
        }

        /// <summary>
        /// Description of the item/product
        /// </summary>
        public string Description;

        /// <summary>
        /// The individual price of the SKU (non-offer value)
        /// </summary>
        public decimal IndividualPrice;

        /// <summary>
        /// The current offer on the SKU item.
        /// </summary>
        public IOffer Offer;
    }
}
