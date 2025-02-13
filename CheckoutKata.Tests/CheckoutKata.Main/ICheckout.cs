namespace CheckoutKata.Main;

public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
}

public class Checkout : ICheckout
{
    public void Scan(string item)
    {
        throw new NotImplementedException();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}