using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Endpoints
{
    public class LoginBehavior : BasicBehavior
    {
        private readonly ICurrentHttpRequest _httpRequest;
        private readonly IFubuRequest _request;
        private readonly IAuthenticationService _service;
        private readonly IAuthenticationSession _session;
        private readonly ILoginSuccessHandler _handler;

        public LoginBehavior(ICurrentHttpRequest httpRequest, IAuthenticationService service,
                             IAuthenticationSession session, IFubuRequest request, ILoginSuccessHandler handler)
            : base(PartialBehavior.Ignored)
        {
            _httpRequest = httpRequest;
            _service = service;
            _session = session;
            _request = request;
            _handler = handler;
        }

        protected override DoNext performInvoke()
        {
            if (!_httpRequest.HttpMethod().Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                return DoNext.Continue;
            }

            var login = _request.Get<LoginRequest>();
            if (_service.Authenticate(login))
            {
                _session.MarkAuthenticated(login.UserName);
                login.Status = LoginStatus.Succeeded;

                _handler.LoggedIn(login);
                return DoNext.Stop;
            }

            login.Status = LoginStatus.Failed;
            login.NumberOfTries++;

            return DoNext.Continue;
        }
    }
}