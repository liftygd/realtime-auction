using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BidChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AuctionBids_HighestBid",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_HighestBid",
                table: "Auctions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Auctions_HighestBid",
                table: "Auctions",
                column: "HighestBid");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AuctionBids_HighestBid",
                table: "Auctions",
                column: "HighestBid",
                principalTable: "AuctionBids",
                principalColumn: "Id");
        }
    }
}
