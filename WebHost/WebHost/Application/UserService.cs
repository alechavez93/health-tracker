using System;
using System.Threading.Tasks;
using FakeItEasy;
using WebHost.DataAccess;
using WebHost.Models;

namespace WebHost.Application
{
    public interface IUserService
    {
        Task<User> FindOne(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> FindOne(Guid id)
        {
            return await this.repository.FindById(id);
        }
    }
}
