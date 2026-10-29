using JwtProject.Web.DTOs.Categories;
using System.Net.Http.Headers;

namespace JwtProject.Web.Services.Categories
{
    public class CategoryService(HttpClient _client) : ICategoryService
    {
        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
            //var token=_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="token")?.Value;
            //_client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);

            var response = await _client.GetFromJsonAsync<List<ResultCategoryDto>>("categories");
            return response;
        }
    }
}
