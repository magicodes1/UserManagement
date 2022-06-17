using System.Security.Claims;

namespace UserManagement.UserManager.Interfaces.Services;

public interface ITokenService
{
    string tokenGeneration(string userName,string id,List<string> roles);

    IEnumerable<Claim> getCLaim(string token);
}