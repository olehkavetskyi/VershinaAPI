using Core.Entities.Identity;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (userManager.Users.Any())
        {

        }
        else
        {

            var admin = new AppUser
            {
                DisplayName = "Alex",
                Email = "alex@test.com",
                UserName = "alex@test.com",
                Address = new Address
                {
                    FirstName = "Alex",
                    LastName = "Deno",
                    Street = "10 The Street",
                    City = "New York",
                    State = "NY",
                    ZipCode = "20424"
                },
            };

            await userManager.CreateAsync(admin, "Ad8n$");

            await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
        }
    }

    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
    }

} 
