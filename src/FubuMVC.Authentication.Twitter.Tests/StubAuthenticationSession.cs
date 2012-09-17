using System;
using FubuMVC.Authentication.Tickets;

namespace FubuMVC.Authentication.Twitter.Tests
{
    public class StubAuthenticationSession : IAuthenticationSession
    {
        private readonly AuthenticationTicket _ticket;

        public StubAuthenticationSession(AuthenticationTicket ticket)
        {
            _ticket = ticket;
        }

        public void MarkAccessed()
        {
        }

        public string PreviouslyAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        public void MarkAuthenticated(string userName, Action<AuthenticationTicket> continuation)
        {
            continuation(_ticket);
        }

        public void ClearAuthentication()
        {
        }
    }
}