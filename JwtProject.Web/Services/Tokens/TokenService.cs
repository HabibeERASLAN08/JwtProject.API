namespace JwtProject.Web.Services.Tokens
{
    public class TokenService(IHttpContextAccessor _httpContextAccessor) : ITokenService
    {
        public string GetToken => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="token")?.Value;

        public int GetUserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id")?.Value);

        public string GetUserName => _httpContextAccessor.HttpContext.User.FindFirst("userName")?.Value;

        public string GetEmail => _httpContextAccessor.HttpContext.User.FindFirst("email")?.Value;
    }
}
