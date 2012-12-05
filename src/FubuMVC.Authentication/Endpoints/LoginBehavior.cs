using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Endpoints
{
    // TODO -- make this be an ActionFilter
    public class LoginBehavior : BasicBehavior
    {
        private readonly ICurrentHttpRequest _httpRequest;
        private readonly IFubuRequest _request;
        private readonly IAuthenticationService _service;
        private readonly ILoginSuccessHandler _handler;

        public LoginBehavior(ICurrentHttpRequest httpRequest, IAuthenticationService service, IFubuRequest request, ILoginSuccessHandler handler)
            : base(PartialBehavior.Ignored)
        {
            _httpRequest = httpRequest;
            _service = service;
            _request = request;
            _handler = handler;
        }

        protected override DoNext performInvoke()
        {
            // TODO -- WTF is this here for?  Smells like a huge hack that should have been killed earlier
            if (!_httpRequest.HttpMethod().Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                return DoNext.Continue;
            }

            var login = _request.Get<LoginRequest>();
            if (_service.Authenticate(login))
            {
                _handler.LoggedIn(login);
                return DoNext.Stop;
            }

            return DoNext.Continue;
        }
    }
}