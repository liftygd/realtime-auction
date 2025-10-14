using Auction.API;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Infrastructure;
using RealtimeAuction.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

//Middleware
app.UseApiServices();
app.UseCustomExceptionHandling();
await app.Services.InitializeDatabaseAsync();

app.Run();