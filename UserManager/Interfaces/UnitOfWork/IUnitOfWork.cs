using UserManagement.UserManager.Interfaces.Repositories;
using UserManagement.UserManager.Models.Entities;

namespace UserManagement.UserManager.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get;}

    Task SaveChangeAsync(CancellationToken cancellationToken=default);
}