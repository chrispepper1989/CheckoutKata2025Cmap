using CheckoutKata.Main;
using FakeItEasy;

namespace CheckoutKata.Test;

/*
 * Checkout Kata
Implement the code for a checkout system that handles pricing schemes such as “pineapples
cost 50, three pineapples cost 130”.
Implement the code for a supermarket checkout that calculates the total price of a number of
items. In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our
store, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced
individually. In addition, some times are multi-priced: buy n of them and they’ll cost you y
pence. For example, item A might cost 50 individually , but this week we have a special offer:
buy three As and they’ll cost you 130. In fact the prices are:
SKU Unit Price Special Price
A 50 3 for 130
B 30 2 for 45
C 20
D 15
The checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll
recognise the two Bs and price them at 45 (for a total price so far of 95). The pricing changes
frequently, so pricing should be independent to the c
 */

public class CheckoutTest
{
    IItemPriceRespository mockItemPriceRespository = A.Fake<IItemPriceRespository>();
    public CheckoutTest()
    {
        A.CallTo(() => mockItemPriceRespository.GetItemPrice("ItemA")).Returns(50);
        A.CallTo(() => mockItemPriceRespository.GetItemPrice("ItemB")).Returns(30);
        A.CallTo(() => mockItemPriceRespository.GetItemPrice("ItemC")).Returns(20);
        A.CallTo(() => mockItemPriceRespository.GetItemPrice("ItemD")).Returns(15);
        
    }
    [Theory]
    [InlineData ("ItemA", 50)]
    [InlineData ("ItemB", 30)]
    [InlineData ("ItemC", 20)]
    [InlineData ("ItemD", 15)]
    public void WhenOneItemScanned_TotalIsPriceOfItem(string item, int expectedPrice)
    {
        //arrange
        
        ICheckout checkout = new Checkout(mockItemPriceRespository);
        
        //act
        checkout.Scan(item);
        var result  = checkout.GetTotalPrice();
        
        //assert
        Assert.Equal(expectedPrice, result);
    }
    
    /*
     *  "ItemA" = 50
        "ItemB" = 30
        "ItemC" = 20
        "ItemD" = 15
     */
    [Theory]
    [InlineData ( 80, "ItemA", "ItemB")]
    [InlineData ( 100, "ItemB", "ItemC", "ItemA")]
    [InlineData ( 35, "ItemC", "ItemD")]
    [InlineData ( 115, "ItemD", "ItemA","ItemB", "ItemC")]
    public void WhenMultipleItemsScanned_TotalIsSumPriceOfItems( int expectedPrice, params string[] items)
    {
        //arrange
        
        ICheckout checkout = new Checkout(mockItemPriceRespository);
        
        //act
        foreach (var item in items)
        {
            checkout.Scan(item);
        }
        
        var result  = checkout.GetTotalPrice();
        
        //assert
        Assert.Equal(expectedPrice, result);
    }
    
    /*
     * A  3 for 130
     */
    [Theory]
    [InlineData ( 130, "ItemA", "ItemA", "ItemA")]
    public void WhenMultipleItemAIsScanned_TotalPriceTakesIntoAcountOffer( int expectedPrice, params string[] items)
    {
        //arrange
        
        ICheckout checkout = new Checkout(mockItemPriceRespository);
        
        //act
        foreach (var item in items)
        {
            checkout.Scan(item);
        }
        
        var result  = checkout.GetTotalPrice();
        
        //assert
        Assert.Equal(expectedPrice, result);
    }
    
    
    
    /*
     * B  2 for 45
     */
    [Theory]
    [InlineData ( 45, "ItemB", "ItemB")]
    public void WhenMultipleItemBIsScanned_TotalPriceTakesIntoAcountOffer( int expectedPrice, params string[] items)
    {
        //arrange
        
        ICheckout checkout = new Checkout(mockItemPriceRespository);
        
        //act
        foreach (var item in items)
        {
            checkout.Scan(item);
        }
        
        var result  = checkout.GetTotalPrice();
        
        //assert
        Assert.Equal(expectedPrice, result);
    }
}