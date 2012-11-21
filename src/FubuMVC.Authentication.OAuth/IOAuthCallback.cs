namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthCallback
    {
        void Execute(IOAuthSignInSettings signInSettings);
    }

    public class OAuthCallback : IOAuthCallback
    {
        private readonly IOAuthProxy _proxy;
        private readonly IAuthenticationSession _session;
        private readonly IOAuthResponseHandler _handlers;

        public OAuthCallback(IOAuthProxy proxy, IAuthenticationSession session, IOAuthResponseHandler handlers)
        {
            _proxy = proxy;
            _session = session;
            _handlers = handlers;
        }

        public void Execute(IOAuthSignInSettings signInSettings)
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