namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthTokenResponse
    {
        string AccessToken { get; }
        string RefreshToken { get; }
    }
}