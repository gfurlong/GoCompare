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
        /// <param name="offerQty">If the SKU has an offer, this quantity should be the number of items that qualify for the offer</param>
        /// <param name="offerPrice">The price of the SKU if part of an offer</param>
        public Sku(string description, decimal individualPrice, int offerQty = 0, decimal offerPrice = 0m)
        {
            Description = description;
            IndividualPrice = individualPrice;
            OfferQty = offerQty;
            OfferPrice = offerPrice;
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
        /// If there's an offer, this is the number of items that needs to be purchased before that offer is in effect
        /// </summary>
        /// <remarks>
        /// Note that a value of zero, indicates there's no current offers for the SKU and the IndividualPrice should be used.
        /// </remarks>
        public int OfferQty;

        /// <summary>
        /// The sale price of the SKU if the number of items matches a multiple of the <see cref="OfferQty">OfferQty</see> value.
        /// </summary>
        public decimal OfferPrice;
    }
}
