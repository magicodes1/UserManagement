using Microsoft.AspNetCore.Identity;

namespace UserManagement.UserManager.Models.Entities;

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}