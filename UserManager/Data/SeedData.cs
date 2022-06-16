using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new UserManagerDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<UserManagerDbContext>>()))
        {
            if(context.ApplicationRoles.Any()) return;

            context.ApplicationRoles.AddRange(
                new ApplicationRole {Name="ADMIN",NormalizedName="ADMIN"},
                new ApplicationRole {Name="USER",NormalizedName="USER"}
            );

            context.SaveChanges();
        }
    }
}