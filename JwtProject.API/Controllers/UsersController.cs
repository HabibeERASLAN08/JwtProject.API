using JwtProject.API.DTOs.UserDtos;
using JwtProject.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto model)

        {
            //manuel mapping
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,

            };

            var result=await userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //return Created();
            return Ok("Kullanıcı Kaydı Başarılı Oldu");
        }

    }
}
