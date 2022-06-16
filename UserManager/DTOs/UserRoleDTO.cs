namespace UserManagement.UserManager.DTOs;

public class UserRoleDTO
{
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
}