using DecoupledIdentity.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DecoupledIdentity.InMemory
{
    public sealed class UserStore : Store<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {
        private readonly RoleStore _roles;

        public UserStore(RoleStore roleStore)
        {
            _roles = roleStore;
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var role = _roles.Find(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase))
                .ToList()
                .First();

            role.AddUser(user);
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            Delete(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
            => Task.FromResult(Get(userId));

        public Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            var user = Find(u => string.Equals(u.Username, userName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            return Task.FromResult(user);
        }
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult(user.Username?.ToUpper());

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult(user.Password);

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult((IList<string>)_roles
                .Find(r => r.UsersInRole.Any(ur => string.Equals(ur.Id, user.Id, StringComparison.OrdinalIgnoreCase)))
                .Select(r => r.Name)
                .ToList());

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult(user.Username);

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
            => Task.FromResult((IList<User>)_roles.Find(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase))
                .First()
                .UsersInRole);

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
            => Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
            => Task.FromResult(_roles
                .Find(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase))
                .ToList()
                .First()
                .UsersInRole
                .Any(u => string.Equals(u.Id, user.Id, StringComparison.OrdinalIgnoreCase)));

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var role = _roles.Find(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase))
                .First();

            role.RemoveUser(user);
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            // Nothing to do, in memory collection
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
