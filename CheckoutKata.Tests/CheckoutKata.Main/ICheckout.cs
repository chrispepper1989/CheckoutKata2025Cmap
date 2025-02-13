namespace CheckoutKata.Main;

public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
}

public class Checkout : ICheckout
{
    int _totalPrice;
    public void Scan(string item)
    {
        _totalPrice += 50;
    }

    public int GetTotalPrice()
    {
        return _totalPrice;
    }
}