using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Application.Extensions;

public static class AuctionExtensions
{
    public static List<AuctionDto> ToAuctionDtoList(this List<Auction> auctions)
    {
        var list = new List<AuctionDto>();

        foreach (var auction in auctions)
            list.Add(auction.ToAuctionDto());
        
        return list;
    }

    public static AuctionDto ToAuctionDto(this Auction auction)
    {
        var addressDto = new AddressDto(
            auction.Address.AddressLine,
            auction.Address.Country,
            auction.Address.State,
            auction.Address.ZipCode);

        var auctionItemDto = new AuctionItemDto(
            auction.AuctionItem.ItemName,
            auction.AuctionItem.ItemDescription,
            auction.AuctionItem.Amount,
            auction.AuctionItem.Price);

        var auctionBids = new List<AuctionBidDto>();

        foreach (var item in auction.AuctionBids)
        {
            var auctionBid = new AuctionBidDto(
                item.Id.Value,
                item.AuctionId.Value,
                item.BiddingDate,
                item.Price);
            
            auctionBids.Add(auctionBid);
        }
        
        var auctionDto = new AuctionDto(
            auction.Id.Value,
            auction.OwnerId.Value,
            addressDto,
            auction.Status,
            auctionItemDto,
            auction.HighestBid?.Value,
            auction.HighestBidAmount,
            auction.StartingPrice,
            auction.MaxPrice,
            auction.PriceIncrement,
            auction.StartDate,
            auction.LatestBidDate,
            auction.AuctionTimeInSeconds,
            auctionBids);

        return auctionDto;
    }
}