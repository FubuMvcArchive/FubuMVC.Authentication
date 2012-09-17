using FubuCore;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterResponseHandler : ITwitterResponseHandler
    {
        private readonly IFubuRequest _request;
        private readonly IOutputWriter _writer;
        private readonly IPartialFactory _factory;

        public TwitterResponseHandler(IFubuRequest request, IOutputWriter writer, IPartialFactory factory)
        {
            _request = request;
            _writer = writer;
            _factory = factory;
        }

        public void Success()
        {
            var login = _request.Get<TwitterLoginRequest>();
            var url = login.Url;
            if (url.IsEmpty())
            {
                url = "~/";
            }

            _writer.RedirectToUrl(url);
        }

        public void Failure()
        {
            var login = _request.Get<TwitterLoginRequest>();
            var request = new LoginRequest
                              {
                                  Url = login.Url,
                                  Message = LoginKeys.Unknown.ToString()
                              };

            _request.Set(request);

            _factory
                .BuildPartial(typeof(LoginRequest))
                .InvokePartial();
        }
    }
}