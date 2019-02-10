using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using WebHost.Models;

namespace WebHost.DataAccess.SqlServerDbStore
{
    public class UserSqlServerRepository : IUserRepository
    {
        public async Task<User> FindById(Guid userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 5, 21),
                Email = "johndoe@email.com",
                Password = "secret"
            };
        }
    }
}
