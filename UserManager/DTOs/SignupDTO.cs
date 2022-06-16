using System.ComponentModel.DataAnnotations;

namespace UserManagement.UserManager.DTOs;

public class SignupDTO
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    [RegularExpression(@"^[0-9]*$")]
    public string PhoneNumber { get; set; } = string.Empty;
}