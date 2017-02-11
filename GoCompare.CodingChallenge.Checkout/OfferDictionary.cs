using System.Collections.Generic;
using GoCompare.CodingChallenge.Checkout.Offers;

namespace GoCompare.CodingChallenge.Checkout
{
    /// <summary>
    /// A dictionary of SKU offers used in the checkout.
    /// </summary>
    public class OfferDictionary : Dictionary<char, IOffer>
    {
    }
}
