
using Basket.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.Baskets.Commands.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart ShoppingCart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("CreateBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Creating basket");
        }
    }
}
