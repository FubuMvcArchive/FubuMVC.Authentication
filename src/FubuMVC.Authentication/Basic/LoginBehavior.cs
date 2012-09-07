using System;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication.Basic
{
    public class LoginBehavior : BasicBehavior
    {
        private readonly ICurrentHttpRequest _httpRequest;
        private readonly IFubuRequest _request;
        private readonly IAuthenticationService _service;
        private readonly IAuthenticationSession _session;
        private readonly IOutputWriter _writer;

        public LoginBehavior(ICurrentHttpRequest httpRequest, IAuthenticationService service,
                             IAuthenticationSession session, IOutputWriter writer, IFubuRequest request)
            : base(PartialBehavior.Ignored)
        {
            _httpRequest = httpRequest;
            _service = service;
            _session = session;
            _writer = writer;
            _request = request;
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

                var url = login.Url;
                if (url.IsEmpty())
                {
                    url = "~/";
                }

                _writer.RedirectToUrl(url);
                login.Status = LoginStatus.Succeeded;

                return DoNext.Stop;
            }

            login.Status = LoginStatus.Failed;
            login.NumberOfTries++;

            return DoNext.Continue;
        }
    }
}