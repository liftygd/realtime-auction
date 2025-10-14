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

app.Run();