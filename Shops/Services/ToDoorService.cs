using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Entities;

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
                product = new Product(name, _idProduct++, price, quantity);
            }

            shop.AddProduct(product);
            if (!_products.Contains(product)) _products.Add(product);
            return product;
        }

        public Product FindProduct(string name)
        {
            return _products.FirstOrDefault(product => product.Name == name);
        }

        public void Purchase(List<Tuple<string, int>> products, double money) // покупка товаров по списку (с наименьшей стоимостью)
        {
            Shop bestShop;
            foreach ((string name, int quantity) in products)
            {
                Product pr = FindProduct(name);
                if (pr == null) throw new Exception("error: the product is out of stock \n");
                bestShop = _shops[0];
                double bestPrice = 1000000000000;
                foreach (Shop shop in _shops)
                {
                    foreach (Product p in shop.Products.Where(p => p.Name == pr.Name && p.Price < bestPrice && quantity < pr.Quantity))
                    {
                        bestPrice = p.Price;
                        bestShop = shop;
                    }
                }

                if (Math.Abs(bestPrice - 1000000000000) == 0) throw new Exception("error: the product is not enough \n");

                money -= bestShop.BuyAProduct(pr, quantity);
                if (money < 0) throw new Exception("error: you didn't have enough money \n");
            }
        }

        public void ChangePrice(Product product, double newPrice, Shop shop)
        {
            Product pr = shop.FindProduct(product.Name);
            if (pr == null)
            {
                throw new Exception("error: can't change the price for a product that doesn't exist \n");
            }

            shop.Products.Remove(pr);
            shop.Products.Add(new Product(pr.Name, pr.Id, newPrice, pr.Quantity));
        }
    }
}