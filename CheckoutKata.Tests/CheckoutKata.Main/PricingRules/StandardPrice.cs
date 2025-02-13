namespace CheckoutKata.Main.PricingRules;

public class StandardPrice : IPricingRule
{
    public int GetProductCost(int itemCost, int count) => itemCost * count;
}

public class SpecialItemAPrice : IPricingRule
{
    public int GetProductCost(int itemCost, int count) => itemCost * count;
}

public class SpecialItemCPrice : IPricingRule
{
    public int GetProductCost(int itemCost, int count) => itemCost * count;
}