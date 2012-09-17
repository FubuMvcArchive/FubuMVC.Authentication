namespace FubuMVC.Authentication.Twitter
{
    public interface ITwitterCallback
    {
        void Execute();
    }

    public class TwitterCallback : ITwitterCallback
    {
        private readonly ITwitterProxy _twitter;
        private readonly IAuthenticationSession _session;
        private readonly ITwitterResponseHandler _handlers;

        public TwitterCallback(ITwitterProxy twitter, IAuthenticationSession session, ITwitterResponseHandler handlers)
        {
            _twitter = twitter;
            _session = session;
            _handlers = handlers;
        }

        public void Execute()
        {
            var authenticated = false;
            _twitter.Process(response =>
            {
                _session.MarkAuthenticated(response.ScreenName, ticket => ticket.UserData = response.AccessToken);
                _handlers.Success();

                authenticated = true;
            });

            if (!authenticated)
            {
                _handlers.Failure();
            }
        }
    }
}