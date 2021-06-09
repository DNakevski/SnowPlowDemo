using System;
using System.Collections.Generic;
using SnowplowShoppingApp.Models;

namespace SnowplowShoppingApp.Services
{
    public interface ITrackingService
    {
        void TrackUserLoginEvent(string userEmail);
        void TrackUserLogoutEvent(string userEmail);
        void TrackUserUnsuccessfulLoginEvent(string userEmail);

        void TrackPageViewEvent(string pageUrl, string pageTitle);

        void TrackUserOrderEvent(User user, List<CartItem> items);
        void TrackCartActionEvent(Guid userId, Product product, int quantity, string action);
    }
}
