using System;
using System.Threading;
using System.Threading.Tasks;
using WebHost.Models;

namespace WebHost.DataAccess
{
    public interface IUserRepository
    {
        Task<User> FindById(Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
