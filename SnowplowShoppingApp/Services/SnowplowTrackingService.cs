using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Snowplow.Tracker;
using Snowplow.Tracker.Emitters;
using Snowplow.Tracker.Endpoints;
using Snowplow.Tracker.Models;
using Snowplow.Tracker.Models.Adapters;
using Snowplow.Tracker.Models.Events;
using Snowplow.Tracker.Queues;
using Snowplow.Tracker.Storage;
using SnowplowShoppingApp.Models;

namespace SnowplowShoppingApp.Services
{
    public class SnowplowTrackingService : ITrackingService
    {
        private readonly SnowplowConfig _config;

        public SnowplowTrackingService(IOptions<SnowplowConfig> config)
        {
            _config = config.Value;
        }

        public Tracker TrackerInstance
        {
            get
            {
                //if the tracker instance is started return the started instance
                if (Tracker.Instance.Started) return Tracker.Instance;

                //else, configure the tracker, start it and return it
                var endpoint = new SnowplowHttpCollectorEndpoint(_config.Endpoint, method: HttpMethod.POST);
                var storage = new LiteDBStorage(_config.LiteDbStorage);
                var queue = new PersistentBlockingQueue(storage, new PayloadToJsonString());
                var emitter = new AsyncEmitter(endpoint, queue);
                var subject = new Subject().SetLang(_config.Language).SetPlatform(Platform.Web);
                Tracker.Instance.Start(emitter, subject, trackerNamespace: _config.TrackerNamespace, appId: _config.AppId);

                return Tracker.Instance;
            }
        }

        public void TrackUserLoginEvent(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-login-success")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackUserLogoutEvent(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-logout")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackUserUnsuccessfulLoginEvent(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-login-fail")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackPageViewEvent(string pageUrl, string pageTitle)
        {
            TrackerInstance.Track(new PageView()
                .SetPageUrl(pageUrl)
                .SetPageTitle(pageTitle)
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());

        }

        public void TrackUserOrderEvent(User user, List<CartItem> items)
        {
            var eCommerceTransactionItems = new List<EcommerceTransactionItem>();
            foreach (var item in items)
            {
                eCommerceTransactionItems.Add(new EcommerceTransactionItem()
                    .SetSku($"{item.Product.Category}-{item.Product.ProductName}-{item.Product.ProductId}")
                    .SetPrice(item.Product.Price)
                    .SetQuantity(item.Quantity)
                    .SetName(item.Product.ProductName)
                    .SetCategory(item.Product.Category)
                    .Build());
            }

            var orderId = new Guid().ToString();
            var totalValue = items.Sum(x => x.Product.Price);

            TrackerInstance.Track(new EcommerceTransaction()
                .SetOrderId(orderId)
                .SetTotalValue(totalValue)
                .SetShipping(10)
                .SetCurrency("GBP")
                .SetCountry(user.Country)
                .SetState(user.State)
                .SetCity(user.City)
                .SetItems(eCommerceTransactionItems)
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackCartActionEvent(Guid userId, Product product, int quantity, string action)
        {
            var productSku = $"{product.Category}-{product.ProductName}-{product.ProductId}";

            var eventDict = new Dictionary<string, object>();
            eventDict.Add("product_sku", productSku);
            eventDict.Add("user_id", userId.ToString());
            eventDict.Add("price", product.Price);
            eventDict.Add("quantity", quantity);
            eventDict.Add("action", action);

            var eventData = new SelfDescribingJson("iglu:self.host.iglu/cart_action_event/jsonschema/1-0-0", eventDict);
            TrackerInstance.Track(new SelfDescribing()
                .SetEventData(eventData)
                .Build());
            ;
        }
    }
}
