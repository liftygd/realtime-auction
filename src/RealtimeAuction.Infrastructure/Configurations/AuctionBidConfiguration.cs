using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Infrastructure.Configurations;

internal class AuctionBidConfiguration : IEntityTypeConfiguration<AuctionBid>
{
    public void Configure(EntityTypeBuilder<AuctionBid> builder)
    {
        builder.HasKey(ab => ab.Id);
        builder.Property(ab => ab.Id).HasConversion(
            bidId => bidId.Value,
            dbId => BidId.Create(dbId));
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(ab => ab.UserId)
            .IsRequired();

        builder.Property(ab => ab.BiddingDate).IsRequired();
        builder.Property(ab => ab.Price).IsRequired();
    }
}