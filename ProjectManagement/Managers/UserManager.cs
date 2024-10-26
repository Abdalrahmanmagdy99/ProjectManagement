using Microsoft.AspNetCore.Identity;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using System.Security.Claims;

namespace ProjectManagement.Managers
{
    public class UserManager
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(
              UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<string>> GetUserRoleAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return new List<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId,out int intUserId))
                return 0;

            return intUserId;
        }

        public string GetCurrentUserName()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
                return string.Empty;
            return userName;
        }
        public async Task<bool> IsManagerUser(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, RolesNames.Manager);
        }

        public async Task<bool> IsEmployeeUser(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, RolesNames.Employee);
        }
    }
}
