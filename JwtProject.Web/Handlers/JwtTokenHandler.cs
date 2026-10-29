using JwtProject.Web.Services.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace JwtProject.Web.Handlers
{
    public class JwtTokenHandler(ITokenService _tokenService,
                            IHttpContextAccessor _httpContextAccessor):DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GetToken);
            var response=await base.SendAsync(request, cancellationToken);

            if(response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                _httpContextAccessor.HttpContext.Response.Redirect("/Auth/Login");
            }
            return response;
        }

    }
}
