using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.DataResponse;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Interfaces.Services;

public interface IUserService
{
    Task<DataResponse> List();
    Task Update(string id,UpdateUserDTO updateUserDTO,string token);
}