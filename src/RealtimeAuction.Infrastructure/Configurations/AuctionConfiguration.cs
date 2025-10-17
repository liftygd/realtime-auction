using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeAuction.Domain.Enums;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Infrastructure.Configurations;

internal class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        //Id
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(
            auctionId => auctionId.Value,
            dbId => AuctionId.Create(dbId));
        
        //OwnerId
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.OwnerId)
            .IsRequired();

        //Address
        builder.ComplexProperty(
            a => a.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });
        
        //Status
        builder.Property(a => a.Status)
            .HasDefaultValue(AuctionStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (AuctionStatus) Enum.Parse(typeof(AuctionStatus), dbStatus));
        
        //Auction Item
        builder.ComplexProperty(
            a => a.AuctionItem, itemBuilder =>
            {
                itemBuilder.Property(i => i.ItemName)
                    .HasMaxLength(AuctionItem.MAX_NAME_LENGTH)
                    .IsRequired();
                
                itemBuilder.Property(i => i.ItemDescription)
                    .HasMaxLength(AuctionItem.MAX_DESCRIPTION_LENGTH)
                    .IsRequired();
                
                itemBuilder.Property(i => i.Amount).IsRequired();
                itemBuilder.Property(i => i.Price).IsRequired();
            });
        
        //Highest bid
        builder.Property(a => a.HighestBid).HasConversion(
            bidId => bidId!.Value,
            dbId => BidId.Create(dbId));
        
        //Pricing
        builder.Property(a => a.HighestBidAmount);
        builder.Property(a => a.StartingPrice);
        builder.Property(a => a.MaxPrice);
        builder.Property(a => a.PriceIncrement);
        
        //Date
        builder.Property(a => a.StartDate);
        builder.Property(a => a.LatestBidDate);
        builder.Property(a => a.AuctionTimeInSeconds);
        
        //Bids
        builder.HasMany(a => a.AuctionBids)
            .WithOne()
            .HasForeignKey(a => a.AuctionId);
    }
}