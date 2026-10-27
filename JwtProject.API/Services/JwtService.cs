using JwtProject.API.Entities;
using JwtProject.API.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace JwtProject.API.Services
{
    public class JwtService : IJwtService
    {

        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<AppUser> _userManager;
        public JwtService(IOptions<JwtOptions> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager= userManager;
        }
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("userName",user.UserName),
                new Claim("id",user.Id.ToString()),
                new Claim("email",user.Email)

               // new Claim(ClaimTypes.Name,user.UserName),
               // new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               // new Claim(ClaimTypes.Email,user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //kullanıcıya ait rolleri çekip claime ekleyeceğiz.

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );
            var token=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
