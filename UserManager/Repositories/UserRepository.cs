using Microsoft.EntityFrameworkCore;
using UserManagement.UserManager.Data;
using UserManagement.UserManager.DTOs;
using UserManagement.UserManager.Interfaces.Repositories;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagerDbContext _context;

    public UserRepository(UserManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> List()
    {
        var users = await _context.Users.Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                            .ToListAsync();
        return users;
    }

    public async Task Update(string id, UpdateUserDTO updateUserDTO)
    {
        var user = await _context.Users.Where(p => p.Id == id).FirstOrDefaultAsync();

        user!.UserName = updateUserDTO.UserName;
        user!.FullName = updateUserDTO.FullName;
        user!.PhoneNumber = updateUserDTO.PhoneNumber;
    }
}