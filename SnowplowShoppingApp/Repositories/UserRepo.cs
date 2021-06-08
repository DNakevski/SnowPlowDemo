using SnowplowShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Repositories
{
    public class UserRepo : IUserRepo
    {
        private List<User> _users;

        public UserRepo()
        {
            SeedInitialUsers();
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            //mimic async execution with Task.FromResult
            return await Task.FromResult(_users); 
        }

        /// <summary>
        /// true: if email and password match
        /// false: if email and password don't match
        /// </summary>
        /// <returns>bool</returns>
        public async Task<User> LoginAsync(string email, string password)
        {
            //mimic async execution with Task.FromResult
            return await Task.FromResult(_users.FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        private void SeedInitialUsers()
        {
            _users = new List<User>
            {
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Scott",
                    Surname = "Walker",
                    Email = "scott@mail.com",
                    Password = "Scott*123"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Scott",
                    Surname = "Walker",
                    Email = "scott@mail.com",
                    Password = "Scott*123"
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Alice",
                    Surname = "Williams",
                    Email = "alice@mail.com",
                    Password = "Alice*123"
                }
            };
        }
    }
}
