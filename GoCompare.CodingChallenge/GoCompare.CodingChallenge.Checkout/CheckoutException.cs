using System;
using System.Diagnostics.CodeAnalysis;

namespace GoCompare.CodingChallenge.Checkout
{
    [ExcludeFromCodeCoverage]
    public class CheckoutException : Exception
    {
        public CheckoutException() 
            : base()
        {}

        public CheckoutException(string message) 
            : base(message)
        {}

        public CheckoutException(string message, Exception inner)
            : base(message, inner)
        {}
    }
}