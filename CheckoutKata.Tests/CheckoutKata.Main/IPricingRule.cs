namespace CheckoutKata.Main;

public interface IPricingRule
{
    int GetProductCost(int itemCost, int count);
}