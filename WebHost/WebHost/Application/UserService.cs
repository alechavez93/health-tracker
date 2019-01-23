using System;
using System.Threading.Tasks;
using FakeItEasy;
using WebHost.Models;

namespace WebHost.Application
{
    public interface IUserService
    {
        Task<User> FindOne(Guid id);
    }

    public class UserService : IUserService
    {
        public Task<User> FindOne(Guid id)
        {
            return new Task<User>(A.Fake<User>);
        }
    }
}
