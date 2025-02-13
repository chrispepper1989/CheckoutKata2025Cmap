namespace CheckoutKata.Main;

public interface IItemPriceRespository
{
    int GetItemPrice(string itemSku, int units);
}

