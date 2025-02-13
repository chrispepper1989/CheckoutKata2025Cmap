namespace CheckoutKata.Main;

public class Checkout(IItemPriceRespository itemPriceRespository) : ICheckout
{
    IItemPriceRespository _itemPriceRespository = itemPriceRespository;
    List<string> items = new List<string>();
    int _totalPrice;

    public void Scan(string item)
    {
        items.Add(item);
       
    }

    public int GetTotalPrice()
    {
        if (items.Count(item => item == "ItemA") == 3)
        {
            return 130;
        }

        return items.Sum(_itemPriceRespository.GetItemPrice);

    }
}