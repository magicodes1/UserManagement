using UserManagement.UserManager.Data;
using UserManagement.UserManager.Interfaces.Repositories;
using UserManagement.UserManager.Interfaces.UnitOfWork;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private IUserRepository _userRepository = null!;

    private readonly UserManagerDbContext _context;

    public UnitOfWork(UserManagerDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
                                => await _context.SaveChangesAsync(cancellationToken);
}