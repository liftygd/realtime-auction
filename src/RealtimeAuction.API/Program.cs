using Auction.API;
using RealtimeAuction.Application;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Infrastructure;
using RealtimeAuction.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();

var app = builder.Build();

//Middleware
app.UseApplicationServices();
app.UseApiServices();
app.UseCustomExceptionHandling();
await app.InitializeDatabaseAsync();

if (app.Environment.IsDevelopment())
    await app.SeedDatabaseAsync();

app.Run();