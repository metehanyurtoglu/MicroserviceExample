using Catalog.API.Exceptions;
using Catalog.API.Features.Products.Queries.GetProductById;
using Catalog.API.Models;
using Core.Application.Abstractions.CQRS;

namespace Catalog.API.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(command => command.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(command => command.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            product = command.Adapt<Product>();

            session.Update(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
