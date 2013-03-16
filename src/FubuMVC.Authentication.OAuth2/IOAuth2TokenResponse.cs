namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2TokenResponse
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}