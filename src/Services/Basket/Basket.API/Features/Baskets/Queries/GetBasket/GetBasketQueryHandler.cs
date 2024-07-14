using Basket.API.Data.Abstractions;
using Basket.API.Models;
using Core.Application.Abstractions.CQRS;

namespace Basket.API.Features.Baskets.Queries.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);

    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasketAsync(query.UserName);

            return new GetBasketResult(basket);
        }
    }
}
