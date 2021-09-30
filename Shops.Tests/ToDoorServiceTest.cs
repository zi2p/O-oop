using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shops.Services;

namespace Shops.Tests
{
    public class Tests
    {
        private ToDoorService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ToDoorService();
        }

        [Test]
        public void DeliveryOfProductsToANewShop()
        {
            Shop shop = _service.AddShop("24h", "Биржевой переулок, 6");
            Product product = _service.AddProduct("зубная паста Paradontax",2, 120, shop);
            Assert.That(shop.FindProduct(product.Name).Item1.Equals(product));
            Assert.That(shop.FindProduct(product.Name).Item3.Equals(120));
            var list = new Tuple<string, int>(product.Name, 2);
            var purchase = new List<Tuple<string, int>>();
            purchase.Add(list);
            double money = 110033;
            _service.Purchase(purchase, money);
        }

        [Test]
        public void ChangeThePriceOfTheProductInTheShop()
        {
            Shop shop = _service.AddShop("24h", "Биржевой переулок, 6");
            Product product = _service.AddProduct("зубная паста Paradontax",2, 120, shop);
            double newPrice = 1.9;
            _service.ChangePrice(product,newPrice,shop);
            Assert.That(shop.FindProduct(product.Name).Item2.Equals(newPrice));
        }

        [Test]
        public void SearchForACheapShop()
        {
            Shop shop1 = _service.AddShop("24h", "ул. Мира, д. 10");
            Shop shop2 = _service.AddShop("Nice Day", "Солнечный пр-кт, д. 2");
            Product product1 = _service.AddProduct("зубная паста Paradontax", 2, 120, shop1);
            var product2 = new Product("ветчина ПАПА МОЖЕТ", 8);
            Product product3 = _service.AddProduct("влажные салфетки Aura", 0.3, 230, shop1);
            _service.AddProduct(product1.Name, 0.4, 400, shop2);
            var list1 = new List<Tuple<string, int>>();
            var list2 = new List<Tuple<string, int>>();
            var pr1 = new Tuple<string, int>(product1.Name, 100000);
            var pr2 = new Tuple<string, int>(product2.Name, 14);
            list1.Add(pr1);
            list2.Add(pr2);
            Assert.Catch<Exception>(() =>
            {
                _service.Purchase(list1,10000000.1);
                _service.Purchase(list2, 1000000.9);
            });
        }

        [Test]
        public void BuyingACertainAmountWithLimitedMoney()
        {
            Shop shop1 = _service.AddShop("24h", "ул. Мира, д. 10");
            Shop shop2 = _service.AddShop("Omigo", "ул. Гоголя, д. 87г");
            Shop shop3 = _service.AddShop("Nice Day", "Солнечный пр-кт, д. 2");
            Product product1 = _service.AddProduct("зубная паста Paradontax", 2, 120, shop1);
            Product product2 = _service.AddProduct("ветчина ПАПА МОЖЕТ", 5, 30, shop1);
            Product product3 = _service.AddProduct("влажные салфетки Aura", 0.3, 230, shop1);
            _service.AddProduct(product1.Name, 1.9, 110, shop2);
            _service.AddProduct(product3.Name, 0.4, 400, shop3);
            _service.AddProduct(product2.Name, 4.9, 12, shop1);
            var list1 = new List<Tuple<string, int>>();
            var list2 = new List<Tuple<string, int>>();
            var pr1 = new Tuple<string, int>(product1.Name, 10);
            var pr2 = new Tuple<string, int>(product2.Name, 14);
            var pr3 = new Tuple<string, int>(product3.Name, 120);
            var pr4 = new Tuple<string, int>(product1.Name, 158);
            list1.Add(pr1);
            list1.Add(pr2);
            list1.Add(pr3);
            list2.Add(pr4);
            list2.Add(pr2);
            const double money1 = 1.9;
            const double money2 = 13438748345.3;
            Assert.Catch<Exception>(() =>
            { 
                _service.Purchase(list1, money1);
                _service.Purchase(list2, money2);
            });
        }
    }
}