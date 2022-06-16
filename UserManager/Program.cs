using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.Data;
using UserManagement.UserManager.Extensions;
using UserManagement.UserManager.Interfaces.Services;
using UserManagement.UserManager.Models.Entities;
using UserManagement.UserManager.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });

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
// auto mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// jwt configurations
builder.Services.AddJwtTokenAuthentication(builder.Configuration);

builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<ITokenService,TokenService>();


var app = builder.Build();

app.Logger.LogInformation("Building Application......................");

//app.Logger.LogInformation("Creating seed data..........................");

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     SeedData.Initialize(services);
// }

//app.Logger.LogInformation("create seed data completed...................");

// Catching global error 500
app.ConfigureExceptionHandler();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Logger.LogInformation("Launching Application..........................");

app.Run();
