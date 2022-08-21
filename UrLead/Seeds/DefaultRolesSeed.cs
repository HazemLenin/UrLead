using Microsoft.AspNetCore.Identity;

namespace UrLead.Seeds
{
    public class DefaultRolesSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Console.WriteLine("Seeding default roles...");
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Sales"));
        }
    }
}
