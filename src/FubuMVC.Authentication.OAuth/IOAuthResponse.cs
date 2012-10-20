namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthResponse
    {
        IOAuthTokenResponse Tokens { get; set; }
        string UserId { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        bool EmailVerified { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string Url { get; set; }
        string Avatar { get; set; }
    }

    public class OAuthResponse : IOAuthResponse
    {
        public IOAuthTokenResponse Tokens { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
    }
}