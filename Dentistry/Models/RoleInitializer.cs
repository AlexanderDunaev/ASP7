using Microsoft.AspNetCore.Identity;

namespace Dentistry.Models
{
    public static class RoleInitializer
    {
        public static async Task<WebApplication> CreateRolesAsync(this WebApplication app, IConfiguration configuration)
        {
            var scope = app.Services.CreateScope();
            var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>))!;
            var roles = configuration.GetSection("Roles").Get<List<string>>();

            foreach (var role in roles!)
            {
                if (!await roleManager!.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>))!;

            string adminEmail = "admin@gmail.com";
            string username = "Admin";
            string password = "Qq12345!";

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new() { Email = adminEmail, UserName = adminEmail, Name=username };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            return app;
        }
    }
}