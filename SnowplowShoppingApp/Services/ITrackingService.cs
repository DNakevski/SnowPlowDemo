using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Services
{
    public interface ITrackingService
    {
        void TrackUserLogin(string userEmail);
        void TrackUserLogout(string userEmail);
        void TrackUserUnsuccessfulLogin(string userEmail);
    }
}
