using DecoupledIdentity.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DecoupledIdentity.InMemory
{
    public sealed class RoleStore : Store<Role>, IRoleStore<Role>
    {
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            Add(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            Delete(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
            => Task.FromResult(Get(roleId));

        public Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken)
            => Task.FromResult(Find(r => string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase)).First());

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
            => Task.FromResult(role.Name);

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
            => Task.FromResult(role.Id);

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
            => Task.FromResult(role.Name);

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            // In-memory store, nothing to do here
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            //Nothing to do
        }
    }
}
