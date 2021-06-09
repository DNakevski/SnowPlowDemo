using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;

namespace SnowplowShoppingApp.Services
{
    public interface ITrackingService
    {
        void TrackUserLogin(string userEmail);
        void TrackUserLogout(string userEmail);
        void TrackUserUnsuccessfulLogin(string userEmail);

        void TrackPageView(string pageUrl, string pageTitle);

        void TrackUserOrder(User user, List<CartItem> items);
    }
}
