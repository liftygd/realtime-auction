using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeAuction.Application.Hubs;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Enums;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Infrastructure.Context;

namespace RealtimeAuction.Infrastructure.Repositories;

public class AuctionRepository(
    IHubContext<AuctionHub, IAuctionClient> hubContext,
    IApplicationDbContext dbContext) 
    : IWriteAuctionRepository, IReadAuctionRepository
{
    public async Task<Guid> CreateAuction(Auction newAuction, CancellationToken cancellationToken = default)
    {
        dbContext.Auctions.Add(newAuction);
        await dbContext.SaveChangesAsync(cancellationToken);
            
        return newAuction.Id.Value;
    }

    public async Task<bool> AddBid(AuctionId auctionId, AuctionBid newBid, CancellationToken cancellationToken = default)
    {
        var auction = await FindAuction(auctionId, cancellationToken);

        auction.Add(newBid.UserId, newBid.BiddingDate, newBid.Price);
        await hubContext.Clients.Group(auctionId.Value.ToString()).AuctionBidAdded(newBid);
        
        if (newBid.Price >= auction.MaxPrice)
        {
            auction.Close();
            await hubContext.Clients.Group(auctionId.Value.ToString()).AuctionStatusUpdated(AuctionStatus.Ended);
        }
            
        dbContext.Auctions.Update(auction);
        await dbContext.SaveChangesAsync(cancellationToken);
            
        return true;
    }

    public async Task<bool> UpdateAuction(AuctionId auctionId, Auction newAuction, CancellationToken cancellationToken = default)
    {
        var auction = await FindAuction(auctionId, cancellationToken);
        
        auction.Update(
            newAuction.Address,
            newAuction.Status,
            newAuction.AuctionItem,
            newAuction.StartingPrice,
            newAuction.MaxPrice,
            newAuction.PriceIncrement,
            newAuction.AuctionTimeInSeconds);
        
        if (newAuction.Status == AuctionStatus.Active)
            await hubContext.Clients.Group(auctionId.Value.ToString()).AuctionStatusUpdated(AuctionStatus.Active);
            
        dbContext.Auctions.Update(auction);
        await dbContext.SaveChangesAsync(cancellationToken);
            
        return true;
    }

    public async Task<List<Auction>> GetAllAuctions(CancellationToken cancellationToken = default)
    {
        return await dbContext.Auctions
            .Include(a => a.AuctionBids)
            .AsNoTracking()
            .OrderBy(a => a.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Auction> GetAuctionById(AuctionId auctionId, CancellationToken cancellationToken = default)
    {
        var auction = await FindAuction(auctionId, cancellationToken);
        return auction;
    }

    public async Task<bool> DeleteAuction(AuctionId auctionId, CancellationToken cancellationToken = default)
    {
        var auction = await FindAuction(auctionId, cancellationToken);
        
        dbContext.Auctions.Remove(auction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    private async Task<Auction> FindAuction(AuctionId auctionId, CancellationToken cancellationToken = default)
    {
        var auction = await dbContext.Auctions
            .Include(a => a.AuctionBids)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == auctionId, cancellationToken);
        
        if (auction == null)
            throw DatabaseExceptions.EntryNonExistent<User>(nameof(auctionId), auctionId.ToString());
        
        return auction;
    }
}