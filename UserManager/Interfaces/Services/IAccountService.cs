using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Models.DataResponse;

namespace UserManagement.UserManager.Interfaces.Services;

public interface IAccountService
{
    Task<DataResponse> Signup (SignupDTO signupDTO);
}