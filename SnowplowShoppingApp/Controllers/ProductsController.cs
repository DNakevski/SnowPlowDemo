using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;
using SnowplowShoppingApp.Repositories;
using SnowplowShoppingApp.Services;

namespace SnowplowShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ITrackingService _trackingService;

        public ProductsController(IProductsRepo productsRepo, ITrackingService trackingService)
        {
            _productsRepo = productsRepo;
            _trackingService = trackingService;
        }

        [HttpGet()]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _trackingService.TrackPageView("api/products", "Get All Products");
            return await _productsRepo.GetAllProductsAsync();
        }
    }
}
