﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;

namespace SnowplowShoppingApp.Repositories
{
    public interface IProductsRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
    }
}
