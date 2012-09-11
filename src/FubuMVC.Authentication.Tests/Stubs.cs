using System.Security.Principal;

namespace FubuMVC.Authentication.Tests
{
    public class StubAuthenticationService : IAuthenticationService
    {
        public bool Authenticate(LoginRequest request)
        {
            throw new System.NotImplementedException();
        }
    }

    public class StubPrincipalBuilder : IPrincipalBuilder
    {
        public IPrincipal Build(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}