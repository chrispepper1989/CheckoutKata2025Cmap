namespace CheckoutKata.Main;

public class Checkout(IItemPriceRespository itemPriceRespository) : ICheckout
{
    IItemPriceRespository _itemPriceRespository = itemPriceRespository;
    int _totalPrice;

    public void Scan(string item)
    {
        _totalPrice += itemPriceRespository.GetItemPrice(item);
    }

    public int GetTotalPrice()
    {
        return _totalPrice;
    }
}