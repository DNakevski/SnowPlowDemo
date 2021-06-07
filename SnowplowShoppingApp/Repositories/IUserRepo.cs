using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;

namespace SnowplowShoppingApp.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> LoginAsync(string email, string password);
    }
}
