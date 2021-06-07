using SnowplowShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Repositories
{
    public class ProductsRepo : IProductsRepo
    {
        private List<Product> _products;

        public ProductsRepo()
        {
            SeedInitialProducts();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            //mimic async execution with Task.FromResult
            return await Task.FromResult(_products);
        }

        private void SeedInitialProducts()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Clothing",
                    ProductName = "Jacket",
                    Description = "Black winter jacket",
                    Price = 150,
                    Stock = 10
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Clothing",
                    ProductName = "T-Shirt",
                    Description = "White t-shirt",
                    Price = 25,
                    Stock = 10
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Accessories",
                    ProductName = "Belt",
                    Description = "Brown leather belt",
                    Price = 45,
                    Stock = 10
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Accessories",
                    ProductName = "Sunglasses",
                    Description = "Tony Stark like sunglasses",
                    Price = 60,
                    Stock = 10
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Shoes",
                    ProductName = "Boots",
                    Description = "Black boots",
                    Price = 220,
                    Stock = 10
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Category = "Shoes",
                    ProductName = "Sneakers",
                    Description = "White Nike sneakers",
                    Price = 180,
                    Stock = 10
                }
            };
        }
    }
}
