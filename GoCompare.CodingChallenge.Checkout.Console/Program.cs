using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCompare.CodingChallenge.Checkout.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var skuIds = args.Any() ? args[0] : "";

            try
            {
                var checkout = new Checkout();

                Console.WriteLine(string.Concat("Total Price: ", checkout.TotalPrice(skuIds)));
            }
            catch (CheckoutException ex)
            {
                Console.Error.WriteLine("Checkout Error: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected Error: {0}", ex.Message);
            }
        }
    }
}
