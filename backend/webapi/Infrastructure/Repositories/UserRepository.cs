using Microsoft.EntityFrameworkCore;
using webapi.Application.Repositories;
using webapi.Entities;
using webapi.Infrastructure.Database;
using webapi.Infrastructure.Database.Models;

namespace webapi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyAppDbContext _dbContext;

        public UserRepository(MyAppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Guid> AddAsync(User user)
        {
            var dbe = new UserDBE
            {
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName
            };

            await _dbContext.Users.AddAsync(dbe);
            return dbe.Id;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var dbe = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (dbe == null) return null;

            return new User
            {
                Id = dbe.Id,
                Username = dbe.Username,
                Password = dbe.Password,
                FullName = dbe.FullName
            };
        }
    }
}
