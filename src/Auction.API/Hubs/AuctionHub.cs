using Auction.API.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Auction.API.Hubs;

public interface IAuctionClient
{
    Task AuctionBidAdded();
    Task AuctionStatusUpdated();
}

public class AuctionHub : Hub<IAuctionClient>
{
    public async Task JoinAuctionGroup(AuctionConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{connection.AuctionId}");
    }
    
    public async Task LeaveAuctionGroup(AuctionConnection connection)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction-{connection.AuctionId}");
    }
}