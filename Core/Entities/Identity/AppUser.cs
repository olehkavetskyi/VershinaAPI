using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public Roles Role { get; set; } = Roles.User;
}
