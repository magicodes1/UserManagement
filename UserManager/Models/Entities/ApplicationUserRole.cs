using Microsoft.AspNetCore.Identity;

namespace UserManagement.UserManager.Models.Entities;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ApplicationRole Role { get; set; } = null!;
}