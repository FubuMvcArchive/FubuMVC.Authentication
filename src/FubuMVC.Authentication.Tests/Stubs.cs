using System.Security.Principal;
using FubuMVC.Authentication.Membership;

namespace FubuMVC.Authentication.Tests
{
    public class StubAuthenticationService : IAuthenticationService
    {
        public bool Authenticate(LoginRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IPrincipal Build(string userName)
        {
            throw new System.NotImplementedException();
        }
    }

}