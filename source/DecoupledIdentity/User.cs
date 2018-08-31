using System;

namespace DecoupledIdentity.Core
{
    public class User : CoreEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
