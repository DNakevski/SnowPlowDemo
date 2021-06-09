using System;

namespace SnowplowShoppingApp.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
