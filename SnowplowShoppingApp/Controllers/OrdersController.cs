using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;
using SnowplowShoppingApp.Repositories;
using SnowplowShoppingApp.Services;

namespace SnowplowShoppingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;
        private readonly IUserRepo _usersRepo;
        private static readonly Dictionary<Guid, List<CartItem>> CartItems = new Dictionary<Guid, List<CartItem>>();

        private readonly ITrackingService _trackingService;

        public OrdersController(IProductsRepo productsRepo, IUserRepo userRepo, ITrackingService trackingService)
        {
            _productsRepo = productsRepo;
            _usersRepo = userRepo;
            _trackingService = trackingService;
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

            //track the add to cart event
            _trackingService.TrackCartActionEvent(userId, product, addToCartModel.Quantity, "add");

            return Ok();
        }

        [HttpGet("cartitems/{userId}")]
        public IEnumerable<CartItem> GetCartItemsForUser(Guid userId)
        {
            _trackingService.TrackPageViewEvent("orders/cartitems", "Get cart items for user");
            return CartItems.ContainsKey(userId) ? CartItems[userId] : new List<CartItem>();
        }

        [HttpDelete("cartitems")]
        public ActionResult RemoveFromCart([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            var cartItem = CartItems[userId]?.FirstOrDefault(x => x.Product.ProductId == productId);
            if (cartItem == null)
                return BadRequest("Some of the parameters are not valid!");

            CartItems[userId].Remove(cartItem);

            //track the remove from cart event
            _trackingService.TrackCartActionEvent(userId, cartItem.Product, cartItem.Quantity, "remove");
            return Ok();
        }

        [HttpPost("makeorder")]
        public async Task<ActionResult> MakeOrder(MakeOrderModel makeOrderModel)
        {
            if (!CartItems.ContainsKey(makeOrderModel.UserId))
                return BadRequest("The specified user doesn't exist");
            
            //track the order
            var user = await _usersRepo.GetUserByIdAsync(makeOrderModel.UserId);
            var items = CartItems[user.UserId];

            //track the make order event
            _trackingService.TrackUserOrderEvent(user, items);

            CartItems.Remove(makeOrderModel.UserId);
            return Ok();
        }
    }
}
