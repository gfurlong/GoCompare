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
        /// <param name="skuId"></param>
        /// <param name="individualPrice"></param>
        /// <param name="offerQty"></param>
        /// <param name="offerPrice"></param>
        public Sku(char skuId, float individualPrice, int offerQty = 0, float offerPrice = 0)
        {
            SkuId = skuId;
            IndividualPrice = individualPrice;
            OfferQty = offerQty;
            OfferPrice = offerPrice;
        }

        /// <summary>
        /// The SKU identifier, e.g. "A", "B", etc.
        /// </summary>
        public char SkuId;

        /// <summary>
        /// The individual price of the SKU (non-offer value)
        /// </summary>
        public float IndividualPrice;

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
        public float OfferPrice;
    }
}
