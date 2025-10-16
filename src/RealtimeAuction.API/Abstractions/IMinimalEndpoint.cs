namespace Auction.API.Abstractions;

public interface IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app);
}