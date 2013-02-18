using System;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Endpoints
{
    // TTODO -- change this to an ActionFilter
    public class LoginBehavior : BasicBehavior
    {
        private readonly ICurrentHttpRequest _httpRequest;
        private readonly IFubuRequest _request;
        private readonly IAuthenticationService _service;
        private readonly ILoginSuccessHandler _handler;
        private readonly ILoginAuditor _auditor;

        public LoginBehavior(ICurrentHttpRequest httpRequest, IAuthenticationService service, IFubuRequest request, ILoginSuccessHandler handler, ILoginAuditor auditor)
            : base(PartialBehavior.Ignored)
        {
            _httpRequest = httpRequest;
            _service = service;
            _request = request;
            _handler = handler;
            _auditor = auditor;
        }

        protected override DoNext performInvoke()
        {
            // This is here to make sure that you don't even bother checking the login if it's 
            // a GET
            if (!_httpRequest.HttpMethod().Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                return DoNext.Continue;
            }

            var login = _request.Get<LoginRequest>();
            var authenticated = _service.Authenticate(login);
            _auditor.Audit(login);

            if (authenticated)
            {
                _handler.LoggedIn(login);
                return DoNext.Stop;
            }

            return DoNext.Continue;
        }
    }
}