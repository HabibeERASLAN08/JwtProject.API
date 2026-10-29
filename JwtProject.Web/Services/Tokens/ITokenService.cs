namespace JwtProject.Web.Services.Tokens
{
    public interface ITokenService
    {
        public string  GetToken { get; }
        public int  GetUserId { get; }
        public string  GetUserName { get; }
        public string  GetEmail { get; }
    }
}
