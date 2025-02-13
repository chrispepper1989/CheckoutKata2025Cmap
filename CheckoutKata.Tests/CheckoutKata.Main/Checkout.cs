namespace CheckoutKata.Main;

public class Checkout(IItemPriceRespository itemPriceRespository) : ICheckout
{
    private readonly List<string> _items = [];

    public void Scan(string item)
    {
        _items.Add(item);
    }

    public int GetTotalPrice()
    {
        // group all the items by their name (SKU) and then apply pricing rules
        var itemGroups = _items.GroupBy(x => x);
        var priceSums = itemGroups.Sum(itemGroup =>
        {
            //lets put everyting into sensible names
            var sku = itemGroup.Key;
            var units = itemGroup.Count();
            //get  price
            return itemPriceRespository.GetItemPrice(sku, units);
            
        });
        
        return priceSums;
    }
}