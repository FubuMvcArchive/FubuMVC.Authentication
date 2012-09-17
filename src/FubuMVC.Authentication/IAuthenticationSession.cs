using System;
using FubuMVC.Authentication.Tickets;

namespace FubuMVC.Authentication
{
    public interface IAuthenticationSession
    {
        void MarkAccessed();
        string PreviouslyAuthenticatedUser();
        void MarkAuthenticated(string userName, Action<AuthenticationTicket> continuation);
        void ClearAuthentication();
    }

    public static class SessionExtensions
    {
        public static void MarkAuthenticated(this IAuthenticationSession session, string userName)
        {
            session.MarkAuthenticated(userName, (ticket) => { });
        }
    }
}