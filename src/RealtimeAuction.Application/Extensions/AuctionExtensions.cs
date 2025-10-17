using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

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

        foreach (var bid in auction.AuctionBids)
        {
            var auctionBid = new AuctionBidDto(
                bid.Id.Value,
                bid.AuctionId.Value,
                bid.UserId.Value,
                bid.BiddingDate,
                bid.Price);
            
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

    public static Auction ToAuction(this AuctionDto auctionDto, Guid id)
    {
        var address = Address.Create(
            auctionDto.Address.AddressLine,
            auctionDto.Address.Country,
            auctionDto.Address.State,
            auctionDto.Address.ZipCode);

        var auctionItem = AuctionItem.Create(
            auctionDto.AuctionItem.ItemName,
            auctionDto.AuctionItem.ItemDescription,
            auctionDto.AuctionItem.Amount,
            auctionDto.AuctionItem.Price);
        
        var auction = Auction.Create(
            AuctionId.Create(id), 
            UserId.Create(auctionDto.OwnerId), 
            address,
            auctionItem,
            auctionDto.StartingPrice,
            auctionDto.MaxPrice,
            auctionDto.PriceIncrement,
            auctionDto.AuctionTimeInSeconds);
        
        auction.Update(
            address, 
            auctionDto.Status, 
            auctionItem, 
            auctionDto.StartingPrice,
            auctionDto.MaxPrice, 
            auctionDto.PriceIncrement, 
            auctionDto.AuctionTimeInSeconds);
        
        foreach (var bidDto in auctionDto.AuctionBids)
        {
            var bid = AuctionBid.Create(
                AuctionId.Create(bidDto.AuctionId),
                UserId.Create(bidDto.UserId), 
                bidDto.BidDate,
                bidDto.BidAmount);

            auction.Add(
                bid.UserId,
                bidDto.BidDate,
                bid.Price);
        }
        
        return auction;
    }
}