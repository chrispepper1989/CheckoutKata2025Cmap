namespace CheckoutKata.Main;

public interface IPricingRuleRepository
{
    IPricingRule GetDiscountPricingRule(string itemSku);
}