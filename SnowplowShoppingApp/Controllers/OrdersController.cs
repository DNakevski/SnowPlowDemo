using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnowplowShoppingApp.Models;
using SnowplowShoppingApp.Repositories;

namespace SnowplowShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;
        private readonly IUserRepo _usersRepo;
        private static readonly Dictionary<Guid, List<CartItem>> CartItems = new Dictionary<Guid, List<CartItem>>();

        public OrdersController(IProductsRepo productsRepo, IUserRepo userRepo)
        {
            _productsRepo = productsRepo;
            _usersRepo = userRepo;
        }

        [HttpPost("cartitems")]
        public async Task<IActionResult> AddToCart(AddToCartModel addToCartModel)
        {
            var userId = new Guid(addToCartModel.UserId);
            var productId = new Guid(addToCartModel.ProductId);

            var user = await _usersRepo.GetUserByIdAsync(userId);
            var product = await _productsRepo.GetProductByIdAsync(productId);

            if (user == null || product == null)
                return BadRequest("Some of the parameters are not valid");

            if(!CartItems.ContainsKey(userId))
                CartItems.Add(userId, new List<CartItem>());

            CartItems[userId].Add(new CartItem
            {
                UserId = userId,
                Product = product,
                Quantity = addToCartModel.Quantity
            });

            return Ok();
        }

        [HttpGet("cartitems/{userId}")]
        public IEnumerable<CartItem> GetCartItemsForUser(Guid userId)
        {
            return CartItems.ContainsKey(userId) ? CartItems[userId] : new List<CartItem>();
        }

        [HttpDelete("cartitems")]
        public ActionResult RemoveFromCart([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            var product = CartItems[userId]?.FirstOrDefault(x => x.Product.ProductId == productId);
            if (product == null)
                return BadRequest("Some of the parameters are not valid!");

            CartItems[userId].Remove(product);
            return Ok();
        }
    }
}
