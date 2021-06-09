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
        /// Returns user by id
        /// </summary>
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.UserId == userId));
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
                    UserId = new Guid("111B0228-4D0D-4C23-8B49-01A698857709"),
                    Name = "Scott",
                    Surname = "Walker",
                    Email = "scott@mail.com",
                    Password = "Scott*123"
                },
                new User
                {
                    UserId = new Guid("222B0228-4D0D-4C23-8B49-01A698857709"),
                    Name = "Scott",
                    Surname = "Walker",
                    Email = "scott@mail.com",
                    Password = "Scott*123"
                },
                new User
                {
                    UserId = new Guid("333B0228-4D0D-4C23-8B49-01A698857709"),
                    Name = "Alice",
                    Surname = "Williams",
                    Email = "alice@mail.com",
                    Password = "Alice*123"
                }
            };
        }
    }
}
