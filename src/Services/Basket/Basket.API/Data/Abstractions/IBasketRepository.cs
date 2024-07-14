using Basket.API.Models;

namespace Basket.API.Data.Abstractions
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    }
}
