using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snowplow.Tracker;
using Snowplow.Tracker.Emitters;
using Snowplow.Tracker.Endpoints;
using Snowplow.Tracker.Models;
using Snowplow.Tracker.Models.Adapters;
using Snowplow.Tracker.Models.Events;
using Snowplow.Tracker.Queues;
using Snowplow.Tracker.Storage;
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

            //var endpoint = new SnowplowHttpCollectorEndpoint("127.0.0.1:9090", method: HttpMethod.POST);
            //var storage = new LiteDBStorage("events.db");
            //var queue = new PersistentBlockingQueue(storage, new PayloadToJsonString());
            //var emitter = new AsyncEmitter(endpoint, queue);
            //var subject = new Subject().SetLang("EN").SetPlatform(Platform.Web);
            //Tracker.Instance.Start(emitter, subject, trackerNamespace: "SnowPlowDemo", appId: "SnowPlowShoppingApp_v1");
        }

        [HttpGet()]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            //Tracker.Instance.Track(new PageView()
            //    .SetPageUrl("api/products")
            //    .SetPageTitle("get-all-products")
            //    .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
            //    .Build());

            return await _productsRepo.GetAllProductsAsync();
        }
    }
}
