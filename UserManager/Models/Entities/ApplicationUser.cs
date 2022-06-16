using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.UserManager.Models.Entities;

public class ApplicationUser : IdentityUser
{
    [StringLength(30)]
    [Required]
    public string FullName { get; set; } = string.Empty;

     public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}