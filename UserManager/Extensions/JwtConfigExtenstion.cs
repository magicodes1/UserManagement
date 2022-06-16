using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Models.Jwt;

namespace UserManagement.UserManager.Extensions;

public static class JwtConfigExtenstion
{
    public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));


        services.AddAuthentication(opt =>
       {
           opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfiguration:SecretKey"])),
               ClockSkew = TimeSpan.Zero
           };
           options.Events = new JwtBearerEvents()
           {
               OnAuthenticationFailed = e =>
               {
                   throw new Exception(e.Exception.Message.ToString());
               },
               OnChallenge = e => throw new UnAuthenticationException("You are not authorized to access this resource."),
               OnForbidden = e => throw new ForbiddenException("Your Token is been expired.")
           };
       });

        return services;
    }
}