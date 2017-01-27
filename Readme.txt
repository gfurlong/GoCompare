******** Advice on implementation

The purpose of this test is for you to demonstrate what you would consider to be Production Ready Good Quality code. Spend as much (or little) time as you feel is necessary to demonstrate your opinion of production quality code. 

A console app will do, don't spend all your time designing out a snazzy UI (unless you really want to :-), we won't be considering a UI as part of assessing the quality of the implementation. 

Feel free to implement the solution in either C# or Javascript.

********* Problem Description

We’re going to see how far we can get in implementing a supermarket checkout that calculates the total price of a number of items. 

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our store, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. In addition, some items are multipriced: buy n of them, and they’ll cost you y pounds. For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. The price and offer table:

Item  Price   Offer
--------------------------
A     50       3 for 130
B     30       2 for 45
C     20
D     15

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95).

******* Getting Started

The interface to your checkout can be anything you like but we’d suggest passing in a string of SKUs to begin with. Here are a few unit test assertions to get you started:

Assert.That(0, Is.EqualTo(price_of("")))
Assert.That(50, Is.EqualTo(price_of("A")))
Assert.That(80, Is.EqualTo(price_of("AB")))
Assert.That(115, Is.EqualTo(price_of("CDBA")))
Assert.That(100, Is.EqualTo(price_of("AA")))
Assert.That(130, Is.EqualTo(price_of("AAA")))
Assert.That(175, Is.EqualTo(price_of("AAABB")))



