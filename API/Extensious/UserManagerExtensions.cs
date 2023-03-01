using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Extensious;

public static class UserManagerExtensions
{
    public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

#pragma warning disable CS8603 // Possible null reference return.
        return await userManager.Users.Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await userManager.Users
            .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
#pragma warning restore CS8603 // Possible null reference return.
    }


}
