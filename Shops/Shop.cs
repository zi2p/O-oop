using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops
{
    public class Shop
    {
        public Shop(string name, string address, int id)
        {
            Name = name;
            Bank = 0;
            Products = new List<Tuple<Product, double, int>>();
            Address = address;
            Id = id;
        }

        public string Name { get; }
        public List<Tuple<Product, double, int>> Products { get; }
        public double Bank { get; set; }
        public string Address { get; }
        public int Id { get; }

        public void AddProduct(Product product, double price, int quantity)
        {
            product.AddShop(this);
            Bank -= price * quantity;
            Tuple<Product, double, int> article;
            foreach (Tuple<Product, double, int> products in Products)
            {
                if (products.Item1 != product) continue;
                article = new Tuple<Product, double, int>(product, price, quantity + products.Item3);
                Products.Remove(products);
                Products.Add(article);
                return;
            }

            article = new Tuple<Product, double, int>(product, price, quantity);
            Products.Add(article);
        }

        public Tuple<Product, double, int> FindProduct(string name)
        {
            foreach (var article in Products)
            {
                if (article.Item1.Name == name) return article;
            }

            return null;
        }

        public double BuyAProduct(Product product, int count)
        {
            double summ = 0;
            foreach (Tuple<Product, double, int> tuple in Products)
            {
                if (tuple.Item1 != product) continue;
                Products.Remove(tuple);
                Products.Add(new Tuple<Product, double, int>(tuple.Item1, tuple.Item2, tuple.Item3 - count));
                Bank += tuple.Item2 * count;
                summ = tuple.Item2 * count;
                return summ;
            }

            return summ;
        }
    }
}