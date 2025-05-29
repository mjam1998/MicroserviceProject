namespace Basket.Api.Entites
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
