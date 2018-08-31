using DecoupledIdentity.Core;
using DecoupledIdentity.InMemory;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DecoupledIdentity.Web.Bootstrapping
{
    internal static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddSingleton<IRoleStore<Role>, RoleStore>()
                .AddSingleton<RoleStore>()
                .AddSingleton<IUserStore<User>, UserStore>();

            services.AddIdentity<User, Role>(ConfigureIdentityOptions)
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.ExpireTimeSpan = TimeSpan.FromDays(30));

            return services;
        }

        private static void ConfigureIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }
    }
}
