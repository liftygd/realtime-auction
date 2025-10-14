using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    Birthday_Day = table.Column<int>(type: "integer", nullable: false),
                    Birthday_Month = table.Column<int>(type: "integer", nullable: false),
                    Birthday_Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionBids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BiddingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionBids_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    HighestBid = table.Column<Guid>(type: "uuid", nullable: true),
                    HighestBidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceIncrement = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LatestBidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuctionTimeInSeconds = table.Column<int>(type: "integer", nullable: false),
                    Address_AddressLine = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    Address_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    AuctionItem_Amount = table.Column<int>(type: "integer", nullable: false),
                    AuctionItem_ItemDescription = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    AuctionItem_ItemName = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    AuctionItem_Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_AuctionBids_HighestBid",
                        column: x => x.HighestBid,
                        principalTable: "AuctionBids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auctions_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_AuctionId",
                table: "AuctionBids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_UserId",
                table: "AuctionBids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_HighestBid",
                table: "Auctions",
                column: "HighestBid");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_OwnerId",
                table: "Auctions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBids_Auctions_AuctionId",
                table: "AuctionBids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBids_Auctions_AuctionId",
                table: "AuctionBids");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "AuctionBids");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
