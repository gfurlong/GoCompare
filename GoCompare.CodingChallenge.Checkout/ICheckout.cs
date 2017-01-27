using System;
namespace GoCompare.CodingChallenge.Checkout
{
    interface ICheckout
    {
        float TotalPrice(string skuIds);
    }
}
