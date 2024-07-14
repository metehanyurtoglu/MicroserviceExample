using Basket.API.Data.Abstractions;
using Core.Application.Abstractions.CQRS;

namespace Basket.API.Features.Baskets.Commands.DeleteBasket
{
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(command => command.UserName).NotEmpty().WithMessage("User name is required");
        }
    }

    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasketAsync(command.UserName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
