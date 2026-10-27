using JwtProject.Web.DTOs.Categories;

namespace JwtProject.Web.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
    }
}
