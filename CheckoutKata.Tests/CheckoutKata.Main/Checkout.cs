namespace CheckoutKata.Main;



public class Checkout(IItemPriceRespository _itemPriceRespository,IDiscountRuleRepository discountRules) : ICheckout
{

    List<string> items = new List<string>();
    int _totalPrice;

    public void Scan(string item)
    {
        items.Add(item);
       
    }

    public int GetTotalPrice()
    {
        // get all the rules to apply
        var discounts = discountRules.GetAllDiscountRules(items.ToArray());
        var discountedItemsCost = 0;
        var standardItems = new List<string>(items);
        foreach (var discount in discounts)
        {
            var discountedItems = new Stack<string>(discount.ItemsProcessed);
            while (discountedItems.TryPop(out var discountItem))
            {
                //remove exactly 1 of the discount items from our "basket"
                standardItems.Remove(discountItem);
            }

            discountedItemsCost += discount.CostToAdd;
        }

        return standardItems.Sum(_itemPriceRespository.GetItemPrice) + discountedItemsCost;

    }
}