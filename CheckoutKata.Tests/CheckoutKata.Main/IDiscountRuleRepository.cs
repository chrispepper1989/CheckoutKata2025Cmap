namespace CheckoutKata.Main;

public record DiscountRule(string[] ItemsProcessed, int CostToAdd);
public interface IDiscountRuleRepository
{
    IEnumerable<DiscountRule> GetAllDiscountRules(params string[] items);
}