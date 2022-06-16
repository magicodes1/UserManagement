using System.ComponentModel.DataAnnotations;

namespace UserManagement.UserManager.DTOs;

public class SigninDTO
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}