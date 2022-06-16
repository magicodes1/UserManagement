using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.DataResponse;

namespace UserManagement.UserManager.Interfaces.Services;

public interface IRoleService
{
     Task<DataResponse> AddRole(AddRoleDTO addRoleDTO);
}