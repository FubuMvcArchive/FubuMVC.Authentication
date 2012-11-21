namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthTokenResponse
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}