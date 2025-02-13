namespace CheckoutKata.Main;

public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
}