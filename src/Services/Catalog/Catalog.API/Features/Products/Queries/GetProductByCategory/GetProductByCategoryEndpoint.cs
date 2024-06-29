
using Catalog.API.Features.Products.Queries.GetProducts;
using Catalog.API.Models;

namespace Catalog.API.Features.Products.Queries.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{Category}", async (string Category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(Category));

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get products by category");
        }
    }
}
