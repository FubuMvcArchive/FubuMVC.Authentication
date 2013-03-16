namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2Response
    {
        IOAuth2TokenResponse Tokens { get; set; }
        string UserId { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        bool EmailVerified { get; set; }
        string Name { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string Url { get; set; }
        string Avatar { get; set; }
    }

    public class OAuth2Response : IOAuth2Response
    {
        public IOAuth2TokenResponse Tokens { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
    }
}