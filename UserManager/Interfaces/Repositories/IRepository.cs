using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> List();
    Task Update(string id,UpdateUserDTO updateUserDTO);
}