namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2Callback
    {
        void Execute(IOAuth2SignInSettings signInSettings);
    }

    public class OAuth2Callback : IOAuth2Callback
    {
        private readonly IOAuth2Proxy _proxy;
        private readonly IAuthenticationSession _session;
        private readonly IOAuth2ResponseHandler _handlers;

        public OAuth2Callback(IOAuth2Proxy proxy, IAuthenticationSession session, IOAuth2ResponseHandler handlers)
        {
            _proxy = proxy;
            _session = session;
            _handlers = handlers;
        }

        public void Execute(IOAuth2SignInSettings signInSettings)
        {
            var authenticated = false;
            _proxy.Process(signInSettings, response =>
                {
                    // TODO -- Keep the access token (response.AccessToken)
                    _session.MarkAuthenticated(response.Email);
                    _handlers.Success(response);

                    authenticated = true;
                });

            if (!authenticated)
            {
                _handlers.Failure();
            }
        }
    }
}