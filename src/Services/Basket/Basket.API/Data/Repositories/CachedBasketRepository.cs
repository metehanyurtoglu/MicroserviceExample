using Basket.API.Data.Abstractions;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data.Repositories
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
            await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasketAsync(shoppingCart, cancellationToken);

            await distributedCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

            return shoppingCart;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasketAsync(userName, cancellationToken);

            await distributedCache.RemoveAsync(userName, cancellationToken);

            return true;
        }
    }
}
