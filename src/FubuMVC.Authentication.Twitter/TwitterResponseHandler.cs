using FubuCore;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Twitter
{
    public class TwitterResponseHandler : ITwitterResponseHandler
    {
        private readonly IFubuRequest _request;
        private readonly IOutputWriter _writer;
        private readonly IPartialFactory _factory;
        private readonly BehaviorGraph _graph;

        public TwitterResponseHandler(IFubuRequest request, IOutputWriter writer, IPartialFactory factory, BehaviorGraph graph)
        {
            _request = request;
            _writer = writer;
            _factory = factory;
            _graph = graph;
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

            var chain = _graph.BehaviorFor(typeof (LoginRequest));

            _factory
                .BuildPartial(chain)
                .InvokePartial();
        }
    }
}