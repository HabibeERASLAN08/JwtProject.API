using System.Globalization;

namespace JwtProject.Web.DTOs.UserDtos
{
    public class TokenResponseDto
    {
        public string Token { get; set; }

        public DateTime ExpireTime  { get; set; }
    }
}
