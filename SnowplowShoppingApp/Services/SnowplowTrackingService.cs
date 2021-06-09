using System;
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

        public void TrackUserLogin(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-login-success")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackUserLogout(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-logout")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackUserUnsuccessfulLogin(string userEmail)
        {
            TrackerInstance.Track(new Structured()
                .SetCategory("users")
                .SetAction("user-login-fail")
                .SetLabel(userEmail)
                .SetProperty("user-email")
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());
        }

        public void TrackPageView(string pageUrl, string pageTitle)
        {
            TrackerInstance.Track(new PageView()
                .SetPageUrl(pageUrl)
                .SetPageTitle(pageTitle)
                .SetTrueTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                .Build());

        }
    }
}
