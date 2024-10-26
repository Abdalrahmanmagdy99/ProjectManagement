using Microsoft.AspNetCore.Identity;
using ProjectManagement.Constants;
using ProjectManagement.Entities;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagement.Seeders
{
    public class IdentitySeeder
    {
        public static async Task AddRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var roles = RolesNames.Roles;

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
        public static async Task AddManagerTestAccountAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string email = "Manager@Test.com";
            string password = "@Dmin123!";
            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new ApplicationUser
                {
                    Id =1,
                    Email = email,
                    UserName = email
                };
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, RolesNames.Manager);
            }
        }
        public static async Task AddEmployeeTestAccountsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string email = "Employee@Test.com";
            string password = "@Dmin123!";

            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new ApplicationUser
                {
                    Id = 2,
                    Email = email,
                    UserName = email
                };
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, RolesNames.Employee);
            }
            email = "Employee2@Test.com";
            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new ApplicationUser
                {
                    Id = 3,
                    Email = email,
                    UserName = email
                };
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, RolesNames.Employee);
            }
        }
    }
}
