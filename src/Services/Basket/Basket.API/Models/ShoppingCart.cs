namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();
        public decimal TotalPrice => ShoppingCartItems.Sum(x => x.Price * x.Quantity);

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
