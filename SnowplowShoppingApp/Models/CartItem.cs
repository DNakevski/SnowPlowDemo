using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Models
{
    public class CartItem
    {
        public Guid UserId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
