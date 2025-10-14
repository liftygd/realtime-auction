using Auction.Exceptions.Exceptions;

namespace Auction.Domain.ValueObjects;
public record AuctionItem
{
    public const int MAX_NAME_LENGTH = 16;
    public const int MAX_DESCRIPTION_LENGTH = 64;

    public string ItemName { get; } = string.Empty;
    public string ItemDescription { get; } = string.Empty;
    public int Amount { get; } = 0;
    public decimal Price { get; } = 0;

    protected AuctionItem() { }

    private AuctionItem(
        string itemName, 
        string itemDescription, 
        int amount, 
        decimal price)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;
        Amount = amount;
        Price = price;
    }

    public static AuctionItem Create(
        string itemName,
        string itemDescription,
        int amount,
        decimal price)
    {
        if (string.IsNullOrEmpty(itemName))
            throw ErrorExceptions.NullOrEmpty<AuctionItem>(nameof(itemName));

        if (itemDescription == null)
            throw ErrorExceptions.NullOrEmpty<AuctionItem>(nameof(itemDescription));

        if (itemName.Length > MAX_NAME_LENGTH)
            throw ErrorExceptions.StringTooLong<AuctionItem>(nameof(itemName), MAX_NAME_LENGTH);

        if (itemDescription.Length > MAX_DESCRIPTION_LENGTH)
            throw ErrorExceptions.StringTooLong<AuctionItem>(nameof(itemDescription), MAX_DESCRIPTION_LENGTH);

        if (amount <= 0)
            throw ErrorExceptions.ZeroOrNegative<AuctionItem>(nameof(amount));

        if (price <= 0)
            throw ErrorExceptions.ZeroOrNegative<AuctionItem>(nameof(price));

        return new AuctionItem(itemName, itemDescription, amount, price);
    }
}
