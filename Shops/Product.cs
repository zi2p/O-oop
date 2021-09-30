using System;
using System.Collections;
using System.Collections.Generic;

namespace Shops
{
    public class Product
    {
        private List<Shop> _shops;

        public Product(string name, int id)
        {
            Name = name;
            _shops = new List<Shop>();
            Id = id;
        }

        public int Id { get; }
        public string Name { get; }

        public void AddShop(Shop shop)
        {
             if (!_shops.Contains(shop)) _shops.Add(shop);
        }

        public Shop GetShop(int count)
        {
            double bestPrice = 100000000;
            Shop bestShop = null;
            foreach (Shop shop in _shops)
            {
                if (shop.FindProduct(Name).Item3 < count || !(shop.FindProduct(Name).Item2 < bestPrice)) continue;
                bestPrice = shop.FindProduct(Name).Item2;
                bestShop = shop;
            }

            return bestShop;
        }
    }
}