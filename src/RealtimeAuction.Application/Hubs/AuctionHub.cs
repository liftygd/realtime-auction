using Microsoft.AspNetCore.SignalR;
using RealtimeAuction.Domain.Enums;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Application.Hubs;

public interface IAuctionClient
{
    Task AuctionBidAdded(AuctionBid auctionBid);
    Task AuctionStatusUpdated(AuctionStatus status);
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