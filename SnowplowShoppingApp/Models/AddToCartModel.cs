using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Models
{
    public class AddToCartModel
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
