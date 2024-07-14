using Basket.API.Data.Abstractions;
using Basket.API.Exceptions;
using Basket.API.Models;

namespace Basket.API.Data.Repositories
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            session.Store(shoppingCart);
            await session.SaveChangesAsync(cancellationToken);

            return shoppingCart;
        }
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
