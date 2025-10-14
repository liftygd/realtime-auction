namespace RealtimeAuction.Exceptions.Exceptions;

public static class AuctionExceptions
{
    public static ErrorException BidTooSmall<TCaller>(string bidId, decimal price)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_BID_TOO_SMALL", 
            $"Bid '{bidId}' is too small. Current price is {price}.");
    }
    
    public static ErrorException BidIsNotIncrementedCorrectly<TCaller>(string bidId, decimal increment)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_BID_WRONG_INCREMENT", 
            $"Bid '{bidId}' has the wrong increment on the price. Increment set for this auction: {increment}.");
    }

    public static ErrorException AuctionAlreadyActive<TCaller>(string auctionId)
    {
        return new ErrorExceptionWithCaller<TCaller>(
            "ERR_AUCTION_ALREADY_ACTIVE", 
            $"Auction '{auctionId}' cannot be updated since it is already active.");
    }
}