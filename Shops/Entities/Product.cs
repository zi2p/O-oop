namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, int id, double price, int quantity)
        {
            Name = name;
            Id = id;
            Price = price;
            Quantity = quantity;
        }

        public Product(Product product)
        {
            Name = product.Name;
            Id = product.Id;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        public int Id { get; }
        public int Quantity { get; }
        public string Name { get; }
        public double Price { get; }
    }
}