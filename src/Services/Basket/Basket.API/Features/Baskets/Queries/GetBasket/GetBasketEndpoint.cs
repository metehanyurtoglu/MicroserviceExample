using Basket.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.Baskets.Queries.GetBasket
{
    public record GetBasketResponse(ShoppingCart ShoppingCart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName}", async (string UserName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(UserName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get basket");
        }
    }
}
