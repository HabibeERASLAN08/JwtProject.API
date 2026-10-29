using JwtProject.Web.DTOs.UserDtos;
using JwtProject.Web.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace JwtProject.Web.Controllers
{
    public class AuthController(IUserService _userService) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result=await _userService.LoginAsync(loginUserDto); 
            if(!result)
            {
                ModelState.AddModelError("", "Email/Kullanıcı adı veya şifre hatalı.");
                return View(loginUserDto);
            }
            return RedirectToAction("Index","Category");
        }
    }
}
