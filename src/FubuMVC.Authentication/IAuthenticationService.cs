using System.Security.Principal;

namespace FubuMVC.Authentication
{
    public interface IAuthenticationService
    {
        bool Authenticate(LoginRequest request);
        IPrincipal Build(string userName);
    }
}