using Auction.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddSignalR();

var app = builder.Build();

//Middleware
app.MapHub<AuctionHub>("/auction/hub");

app.Run();