using DotNetOpenAuth.OAuth.Messages;

namespace FubuMVC.Authentication.Twitter
{
    public interface ITokenResponse
    {
        string AccessToken { get; }
    }

    public class TokenResponse : ITokenResponse
    {
        private readonly AuthorizedTokenResponse _response;

        public TokenResponse(AuthorizedTokenResponse response)
        {
            _response = response;
        }

        public string AccessToken
        {
            get { return _response.AccessToken; }
        }
    }

    public class TwitterAuthResponse
    {
        public TwitterAuthResponse(int userId, string screenName, ITokenResponse response)
        {
            UserId = userId;
            ScreenName = screenName;
            Response = response;
        }

        public ITokenResponse Response { get; private set; }

        public string AccessToken { get { return Response.AccessToken; } }
        public string ScreenName { get; private set; }
        public int UserId { get; private set; }
    }
}