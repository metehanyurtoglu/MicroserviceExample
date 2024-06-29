
using Catalog.API.Features.Products.Commands.UpdateProduct;

namespace Catalog.API.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id));

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response.IsSuccess);
            })
            .WithName("DeleteProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Deletes products.");
        }
    }
}
