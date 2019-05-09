using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Services
{
    public class RoleService
    {
        private const string AdminEmailKey = "AdminEmail";

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public RoleService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task InitializeRolesAsync()
        {
            if (await _roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (await _roleManager.FindByNameAsync(UserRoles.User) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var adminEmail = _configuration[AdminEmailKey];
            var admin = await _userManager.FindByEmailAsync(adminEmail);

            if (admin != null && !await _userManager.IsInRoleAsync(admin, UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }
        }
    }
}
