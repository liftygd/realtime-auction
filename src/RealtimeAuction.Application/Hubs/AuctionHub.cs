using Microsoft.AspNetCore.SignalR;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Domain.Enums;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Application.Hubs;

public interface IAuctionClient
{
    Task AuctionJoined(string userName);
    Task AuctionLeft(string userName);
    Task AuctionBidAdded(AuctionBid auctionBid);
    Task AuctionStatusUpdated(string status);
}

public class AuctionHub : Hub<IAuctionClient>
{
    public async Task JoinAuctionGroup(AuctionConnection connection)
    {
        var connectionString = AuctionExtensions.GetAuctionConnection(connection.AuctionId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, connectionString);
        await Clients.Group(connectionString).AuctionJoined(connection.UserName);
    }
    
    public async Task LeaveAuctionGroup(AuctionConnection connection)
    {
        var connectionString = AuctionExtensions.GetAuctionConnection(connection.AuctionId);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, connectionString);
        await Clients.Group(connectionString).AuctionLeft(connection.UserName);
    }
}