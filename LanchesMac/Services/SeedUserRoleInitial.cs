using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _senha = "Gauss@2023";
        private readonly string _userName = "usuario@localhost";
        private readonly string _adminName = "admin@localhost";
        public SeedUserRoleInitial(
                                    UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        /// <summary>
        /// Deveria ter feito async ex: public async Task SeedRoles()
        /// </summary>
        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                };
                var roleResult = _roleManager.CreateAsync(role).Result; // se fosse async: var roleResult = await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                var roleResult = _roleManager.CreateAsync(role).Result;
            }
        }


        /// <summary>
        /// Deveria ter feito async ex: public async Task SeedUsers()
        /// </summary>
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync(_userName).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = _userName,
                    Email = _userName,
                    NormalizedUserName = _userName.ToUpper(),
                    NormalizedEmail = _userName.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, _senha).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (_userManager.FindByEmailAsync(_adminName).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = _adminName,
                    Email = _adminName,
                    NormalizedUserName = _adminName.ToUpper(),
                    NormalizedEmail = _adminName.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = _userManager.CreateAsync(user, _senha).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
