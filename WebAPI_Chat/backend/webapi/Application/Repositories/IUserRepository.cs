using webapi.Entities;

namespace webapi.Application.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByUsernameAsync(string username);
    }
}
