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

public partial class CheckoutTest
{
    

    // basic price repo used for tests
    public class BasicPriceRepository : IItemPriceRespository
    {
        private readonly Dictionary<string, Func<int, int>> _itemPriceRules = new();
            
            // set up all pricing rules for the test
        public BasicPriceRepository()
        {
            // define a simple factor to create pricing rules
            Func<int, int> CreateTriggerPricingRule(int regularPrice, int specialPrice, int triggerQuantity) => units =>
                (units / triggerQuantity * specialPrice) + units % triggerQuantity * regularPrice;

            
            //Item A Pricing 
            //A s 50 3 for 130
            _itemPriceRules["ItemA"] = CreateTriggerPricingRule(regularPrice:50, specialPrice:130, triggerQuantity:3);
            
            //Item B Pricing 
            //B 30 2 for 45
            _itemPriceRules["ItemB"] = CreateTriggerPricingRule(regularPrice:30, specialPrice:45, triggerQuantity:2);
            
            //Item C Pricing 
            //C 20
            _itemPriceRules["ItemC"] = units => 20 * units;
            
            //C 20
            _itemPriceRules["ItemD"] = units => 15 * units;
        }
        public int GetItemPrice(string item, int units) => _itemPriceRules[item](units);
        
    }
    
    IItemPriceRespository mockItemPriceRespository = new BasicPriceRepository();
    
    private ICheckout _checkout;
    public CheckoutTest()
    {
        //set up standard pricing
        
        
        //set up pricing rules
   
        _checkout = new Checkout(mockItemPriceRespository);
        
    }
    [Theory]
    [InlineData ("ItemA", 50)]
    [InlineData ("ItemB", 30)]
    [InlineData ("ItemC", 20)]
    [InlineData ("ItemD", 15)]
    public void WhenOneItemScanned_TotalIsPriceOfItem(string item, int expectedPrice)
    {
        //arrange
        
        //act
        _checkout.Scan(item);
        var result  = _checkout.GetTotalPrice();
        
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
        
        
        //act
        foreach (var item in items)
        {
            _checkout.Scan(item);
        }
        
        var result  = _checkout.GetTotalPrice();
        
        //assert
        Assert.Equal(expectedPrice, result);
    }
    
    
    
    /*
     * "ItemA" = 50
        "ItemB" = 30
        "ItemC" = 20
        "ItemD" = 15
        
     *  B  2 for 45
     *  A  3 for 130
     */
    [Theory]
    [InlineData ( 45, "ItemB", "ItemB")]
    [InlineData ( 130, "ItemA", "ItemA", "ItemA")]
    [InlineData ( 75, "ItemB", "ItemB", "ItemB")] //special offer plus none special
    public void WhenMultipleItemsAreScanned_TotalPriceTakesIntoAccountOffers( int expectedPrice, params string[] items)
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