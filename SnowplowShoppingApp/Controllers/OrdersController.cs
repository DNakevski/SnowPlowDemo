using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Repositories;

namespace SnowplowShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;

        public OrdersController(IProductsRepo productsRepo)
        {
            _productsRepo = productsRepo;
        }


    }
}
