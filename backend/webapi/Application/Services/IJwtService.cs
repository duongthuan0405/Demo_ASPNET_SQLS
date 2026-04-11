using webapi.Entities;

namespace webapi.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
