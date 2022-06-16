using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.Data;
using UserManagement.UserManager.Extensions;
using UserManagement.UserManager.Models.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<UserManagerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
                .AddEntityFrameworkStores<UserManagerDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
});


var app = builder.Build();

app.Logger.LogInformation("Building Application......................");

// Catching global error 500
app.ConfigureExceptionHandler();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Logger.LogInformation("Launching Application..........................");

app.Run();
