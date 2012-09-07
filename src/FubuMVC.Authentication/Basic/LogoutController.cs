﻿using FubuMVC.Core;
using FubuMVC.Core.Continuations;

namespace FubuMVC.Authentication.Basic
{
    public class LogoutController
    {
        private readonly IAuthenticationSession _session;

        public LogoutController(IAuthenticationSession session)
        {
            _session = session;
        }

        [UrlPattern("logout")]
        public FubuContinuation Logout(LogoutRequest request)
        {
            _session.ClearAuthentication();
            return FubuContinuation.RedirectTo(new LoginRequest());
        }
    }
}