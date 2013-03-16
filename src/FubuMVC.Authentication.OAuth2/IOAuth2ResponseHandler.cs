using FubuCore;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2ResponseHandler
    {
        void Success(IOAuth2Response response);
        void Failure();
    }

    public class OAuth2ResponseHandler<T> : IOAuth2ResponseHandler where T : class, IOAuth2LoginRequest
    {
        private readonly IFubuRequest _request;
        private readonly IOutputWriter _writer;
        private readonly IPartialFactory _factory;
        private readonly BehaviorGraph _graph;

        public OAuth2ResponseHandler(IFubuRequest request, IOutputWriter writer, IPartialFactory factory, BehaviorGraph graph)
        {
            _request = request;
            _writer = writer;
            _factory = factory;
            _graph = graph;
        }

        public void Success(IOAuth2Response response)
        {
            var login = _request.Get<T>();
            var url = login.Url;
            if (url.IsEmpty())
            {
                url = "~/";
            }

            _writer.RedirectToUrl(url);
        }

        public void Failure()
        {
            var login = _request.Get<T>();
            var request = new LoginRequest
                {
                    Url = login.Url,
                    Message = LoginKeys.Unknown.ToString()
                };

            _request.Set(request);

            var chain = _graph.BehaviorFor(typeof(LoginRequest));

            _factory
                .BuildPartial(chain)
                .InvokePartial();
        }
    }
}