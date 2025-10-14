using Auction.API;
using Auction.Domain.ValueObjects;
using Auction.Exceptions;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddSignalR();
builder.Services.AddApiServices();

var app = builder.Build();

//Middleware
app.UseApiServices();
app.UseCustomExceptionHandling();

app.MapPost("/test", async (string itemName, string itemDescription, int amount, decimal price) =>
{
    var test = AuctionItem.Create(itemName, itemDescription, amount, price);
});

app.Run();