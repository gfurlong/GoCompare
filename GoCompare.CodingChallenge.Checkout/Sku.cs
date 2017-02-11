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
        public Sku(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        /// <summary>
        /// Description of the item/product
        /// </summary>
        public string Description;

        /// <summary>
        /// The individual price of the SKU (non-offer value)
        /// </summary>
        public decimal Price;
    }
}
