using JwtProject.Web.DTOs.UserDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtProject.Web.Services.Users
{
    public class UserService(HttpClient _client,IHttpContextAccessor _httpContextAccessor) : IUserService
    {
        public async Task<bool> LoginAsync(LoginUserDto loginUserDto)
        {
            var response = await _client.PostAsJsonAsync("users/login", loginUserDto);

            if(!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result= await response.Content.ReadFromJsonAsync<TokenResponseDto>();

            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(result.Token);
            var claims=token.Claims.ToList();

            claims.Add(new Claim("token", result.Token));

            var claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = result.ExpireTime,
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);

            return true;
        }
    }
}
