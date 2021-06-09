using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Services
{
    public interface ITrackingService
    {
        void RegisterUserLogin(string userEmail);
        void RegisterUserLogout(string userEmail);
        void RegisterUserUnsuccessfulLogin(string userEmail);
    }
}
