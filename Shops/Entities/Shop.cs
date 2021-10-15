using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Entities
{
    public class Shop
    {
        public Shop(string name, string address, int id)
        {
            Name = name;
            Bank = 0;
            Products = new List<Product>();
            Address = address;
            Id = id;
        }

        public string Name { get; }
        public List<Product> Products { get; }
        public double Bank { get; set; }
        public string Address { get; }
        public int Id { get; }

        public void AddProduct(Product product)
        {
            Bank -= product.Price * product.Quantity;
            Product newProduct;
            foreach (Product products in Products.Where(products => products == product))
            {
                Products.Remove(products);
                newProduct = new Product(product.Name, product.Id, product.Price, product.Quantity + products.Quantity);
                Products.Add(newProduct);
                return;
            }

            newProduct = new Product(product);
            Products.Add(newProduct);
        }

        public Product FindProduct(string name)
        {
            return Products.FirstOrDefault(product => product.Name == name);
        }

        public double BuyAProduct(Product product, int count)
        {
            double summ = 0;
            foreach (Product pr in Products.Where(pr => pr == product))
            {
                Products.Remove(pr);
                Products.Add(new Product(pr.Name, pr.Id, pr.Price, pr.Quantity - count));
                Bank += pr.Price * count;
                summ = pr.Quantity * count;
                return summ;
            }

            return summ;
        }
    }
}