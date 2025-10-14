using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            dbId => UserId.Create(dbId));

        builder.Property(u => u.Username)
            .HasMaxLength(User.MAX_USERNAME_LENGTH)
            .IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.EmailAddress).IsRequired();
        builder.HasIndex(u => u.EmailAddress).IsUnique();

        builder.ComplexProperty(
            u => u.Birthday, birthdayBuilder =>
            {
                birthdayBuilder.Property(b => b.Year).IsRequired();
                birthdayBuilder.Property(b => b.Month).IsRequired();
                birthdayBuilder.Property(b => b.Day).IsRequired();
            });
    }
}