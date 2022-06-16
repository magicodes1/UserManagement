using System.ComponentModel.DataAnnotations;

namespace UserManagement.UserManager.DTOs;

public class AddRoleDTO
{
    [Required]
    public string Id { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
}