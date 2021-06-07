using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
