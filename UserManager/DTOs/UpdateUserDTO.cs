using System.ComponentModel.DataAnnotations;

namespace UserManagement.UserManager.DTOs;

public class UpdateUserDTO
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;
    [RegularExpression(@"^[0-9]*$")]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}