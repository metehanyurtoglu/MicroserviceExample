using Basket.API.Data.Abstractions;
using Basket.API.Models;
using Core.Application.Abstractions.CQRS;
using Discount.gRPC;

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

    public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.ShoppingCart, cancellationToken);

            await basketRepository.StoreBasketAsync(command.ShoppingCart, cancellationToken);

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            foreach (var item in shoppingCart.ShoppingCartItems)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                });

                item.Price -= coupon is not null ? coupon.Amount : 0;
            }
        }
    }
}
