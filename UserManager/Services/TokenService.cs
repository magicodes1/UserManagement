using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Models.Jwt;

namespace UserManagement.UserManager.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfig;

    public TokenService(IOptions<JwtConfiguration> options)
    {
        _jwtConfig=options.Value;
    }

    public IEnumerable<Claim> getCLaim(string token)
    {
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        return claims!;
    }

    public string tokenGeneration(string userName, string id, List<string> roles)
    {
       var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,id),
            new Claim(ClaimTypes.Name,userName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokeOptions = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.Now.AddMinutes(50),
           signingCredentials: signinCredentials
       );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }
}