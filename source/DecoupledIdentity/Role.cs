using System;
using System.Collections.Generic;
using System.Linq;

namespace DecoupledIdentity.Core
{
    public class Role : CoreEntity
    {
        private readonly List<User> _usersInRole = new List<User>();

        public string Name { get; set; }
        public IReadOnlyCollection<User> UsersInRole => _usersInRole.AsReadOnly();

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (_usersInRole.Any(u => u.Id == user.Id))
                return;

            _usersInRole.Add(user);
        }

        public void RemoveUser(User user)
        {
            var foundUser = _usersInRole.FirstOrDefault(u => u.Id == user.Id);

            if (foundUser == null)
                return;

            _usersInRole.Remove(foundUser);
        }
    }
}
