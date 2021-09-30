using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Services
{
    public class ToDoorService
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Product> _products = new List<Product>();
        private int _idProduct = 0;
        private int _idShop = 0;

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address, _idShop++);
            if (!_shops.Contains(shop)) _shops.Add(shop);
            return shop;
        }

        public Product AddProduct(string name, double price, int quantity, Shop shop)
        {
            Product product = FindProduct(name);
            if (product == null)
            {
                product = new Product(name, _idProduct++);
                product.AddShop(shop);
            }

            shop.AddProduct(product, price, quantity);
            if (!_products.Contains(product)) _products.Add(product);
            return product;
        }

        public Product FindProduct(string name)
        {
            return _products.FirstOrDefault(product => product.Name == name);
        }

        public void Purchase(List<Tuple<string, int>> products, double money) // покупка товаров по списку (с наименьшей стоимостью)
        {
            foreach (Tuple<string, int> product in products)
            {
                Product pr = FindProduct(product.Item1);
                if (pr == null) throw new Exception("error: the product is out of stock \n");
                money -= pr.GetShop(product.Item2).BuyAProduct(pr, product.Item2);
                if (money < 0) throw new Exception("error: you didn't have enough money \n");
            }
        }

        public void ChangePrice(Product product, double newPrice, Shop shop)
        {
            if (FindProduct(product.Name) == null)
            {
                throw new Exception("error: can't change the price for a product that doesn't exist \n");
            }

            Tuple<Product, double, int> article = shop.FindProduct(product.Name);
            shop.Products.Remove(article);
            shop.Products.Add(new Tuple<Product, double, int>(article.Item1, newPrice, article.Item3));
        }
    }
}