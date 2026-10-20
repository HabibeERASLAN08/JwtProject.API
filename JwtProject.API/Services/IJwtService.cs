using JwtProject.API.Entities;

namespace JwtProject.API.Services
{
    public interface IJwtService
    {
         Task<string> GenerateToken(AppUser user);
    }
}
