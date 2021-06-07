using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;
using SnowplowShoppingApp.Repositories;

namespace SnowplowShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;
        public ProductsController(IProductsRepo productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productsRepo.GetAllProductsAsync();
        }
    }
}
