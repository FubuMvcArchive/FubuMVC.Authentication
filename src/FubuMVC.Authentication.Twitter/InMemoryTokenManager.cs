using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using FubuCore.Util;
using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.Twitter
{
    [Singleton]
    public class InMemoryTokenManager : IConsumerTokenManager
    {
        private readonly OAuthSettings _settings;
        private readonly Cache<string, string> _values;

        public InMemoryTokenManager(OAuthSettings settings)
        {
            _settings = settings;
            _values = new Cache<string, string>(key => null);
        }

        public string GetTokenSecret(string token)
        {
            return _values[token];
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            _values[response.Token] = response.TokenSecret;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            _values.Remove(requestToken);
            _values[accessToken] = accessTokenSecret;
        }

        public TokenType GetTokenType(string token)
        {
            throw new System.NotImplementedException();
        }

        public string ConsumerKey
        {
            get { return _settings.ConsumerKey; }
        }

        public string ConsumerSecret
        {
            get { return _settings.ConsumerSecret; }
        }
    }
}