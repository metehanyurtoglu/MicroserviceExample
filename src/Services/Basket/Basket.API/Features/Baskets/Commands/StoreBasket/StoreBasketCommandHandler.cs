using Basket.API.Data.Abstractions;
using Basket.API.Models;
using Core.Application.Abstractions.CQRS;

namespace Basket.API.Features.Baskets.Commands.StoreBasket
{
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(command => command.ShoppingCart).NotNull().WithMessage("Cart can not be null");
            RuleFor(command => command.ShoppingCart.UserName).NotEmpty().WithMessage("User name is required");
        }
    }

    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.StoreBasketAsync(command.ShoppingCart, cancellationToken);

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
