using JwtProject.Web.DTOs.UserDtos;

namespace JwtProject.Web.Services.Users
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginUserDto loginUserDto);
    }
}
