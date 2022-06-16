using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Data;

public class UserManagerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
                                        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
    public DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
         base.OnModelCreating(builder);

        builder.Entity<ApplicationUserRole>().HasKey(k => new { k.RoleId, k.UserId });

        builder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(fk => fk.RoleId);

        builder.Entity<ApplicationUserRole>()
               .HasOne(ur => ur.User)
               .WithMany(u => u.UserRoles)
               .HasForeignKey(fk => fk.UserId);
    }
}