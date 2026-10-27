using JwtProject.Web.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtProject.Web.Controllers
{
    [Authorize]
    public class CategoryController(ICategoryService _categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories=await _categoryService.GetAllAsync();

            return View(categories);
        }
    }
}
