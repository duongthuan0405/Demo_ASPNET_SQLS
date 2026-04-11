using webapi.Entities;

namespace webapi.Application.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
