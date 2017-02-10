using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCompare.CodingChallenge.Checkout.ConsoleApp
{
    public class Program
    {
        private static int MaxWidth {
            get {
                return Console.WindowWidth - 1;
            }
        }

        private static void WriteLineCentered(string text)
        {
            var padding = ((Console.WindowWidth / 2) + (text.Length / 2));

            Console.WriteLine(text.PadLeft(padding).PadRight(Console.WindowWidth -1));
        }

        private static void ShowBasketDetails(ICheckout checkout)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            WriteLineCentered("Checkout Basket");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("{0} {1} {2,15}  {3,8}", "Qty".PadLeft(5), "Description".PadRight(20), "Price", "Offer");
            Console.WriteLine("".PadRight(MaxWidth, '='));

            foreach (var basketItem in checkout.Basket)
            {
                var skuId = basketItem.Key;
                var qty = basketItem.Value;

                var sku = checkout.GetSkuDetails(skuId);

                Console.WriteLine("{0} {1} {2,15:£0.00}  {3,8}",
                    basketItem.Value.ToString().PadLeft(5),
                    sku.Description.PadRight(20), sku.IndividualPrice, 
                    (sku.Offer != null) 
                        ? sku.Offer.ToString()
                        : "");
            };

            Console.WriteLine("".PadRight(MaxWidth, '='));
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("      {0} {1,15:£0.00}", "Total Price:".PadRight(20), checkout.TotalPrice());
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {
            var skuIds = args.Any() ? args[0] : "";

            try
            {
                var factory = new CheckoutFactory();
                var checkout = factory.Create();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                ConsoleKeyInfo cki;

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                WriteLineCentered("GoCompare Checkout");

                Console.BackgroundColor = ConsoleColor.Black;
                WriteLineCentered("Press A-D to add items to the basket.");
                WriteLineCentered("Press <Enter> to complete the checkout.");
                Console.WriteLine();

                do
                {
                    cki = Console.ReadKey(true);

                    var skuId = Char.ToUpper(cki.KeyChar);

                    switch(skuId) {
                        case 'A':
                        case 'B':
                        case 'C':
                        case 'D':
                            try
                            {
                                checkout.AddItemToBasket(skuId);
                                Console.Write("{0} ", skuId); 
                            }
                            catch (CheckoutException ex)
                            {
                                Console.Error.WriteLine("Checkout Error: {0}", ex.Message);
                            }

                            break;
                    }
                } while (cki.Key != ConsoleKey.Enter);

                Console.WriteLine();
                Console.WriteLine(); 
                
                // Show the basket details...
                ShowBasketDetails(checkout);

                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected Error: {0}", ex.Message);
            }
        }
    }
}
